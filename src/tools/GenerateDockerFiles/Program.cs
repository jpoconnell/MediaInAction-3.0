using System;
using System.IO;
using System.Text;

//var frontends = new[] {"angular"};
var libraries = new[] {"DelugeRPCClient.Net", "EmbyClient.Dotnet"};
var gateways = new[] {"WebGateway", "WebPublicGateway"};
var services = new[] {"Administration", "Identity", "Emby", "Deluge", "Cmskit" , "File", "Video", "Trakt"};

string WriteCopyStatements(string type, string name)
{
    var t = type;
    var n = name.ToLower();
    var u = "";
    if (name.Length > 0)
    {
         u = char.ToUpper(n[0]) + n[1..]  + "Service";
    }

    var s = new StringBuilder();
    
    string dir = Directory.GetParent(Environment.CurrentDirectory)
        .Parent // bin
        .FullName;

    string myDir2 = "";
    if ((type == "gateways") || (type == "libraries") || (type == "common"))
    {
        myDir2 = dir + "/src/" + type ;
    }
    else if (type == "services")
    {
       myDir2 = dir + "/src/" + type + "/" + name.ToLower();
    }
   
    string[] dir2 =  Directory.GetDirectories(myDir2);
    foreach (var myDir in dir2)
    {
        var myDir3 =Path.GetRelativePath(dir,myDir);
        string[] dirStubs = myDir3.Split('/');
        int totCnt = dirStubs.Count();
        var cnt = 0;
        var project = "";
        foreach (var myStub in dirStubs)
        {
            if (cnt == totCnt -1)
            {
                project = myStub;
            }

            cnt++;
        }
        s.AppendLine($"COPY \"{myDir3}/{project}.csproj\" \"{myDir3}/{project}.csproj\"");
    }
    return s.ToString();
}

// Generates dockerfiles in a consistent mechanism to take best advantage of the caching
string GenerateDockerfile(string type, string name)
{
    var t = type;
    var n = name;
    var u = char.ToUpper(n[0]) + n[1..];
    var s = new StringBuilder();
    var projectName = "";
    s.AppendLine("FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base");
    s.AppendLine("WORKDIR /app");
    s.AppendLine("EXPOSE 80");
    s.AppendLine("EXPOSE 81");
    
    s.AppendLine("");
    s.AppendLine("# add globalization support");
    s.AppendLine("RUN apk add --no-cache icu-libs");
    s.AppendLine("ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false");
    s.AppendLine("");
    s.AppendLine("# add diagnostic tools");
    s.AppendLine("RUN apk add --no-cache curl");
    
    s.AppendLine("");

    s.AppendLine("FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build");
    s.AppendLine("WORKDIR /work");
    s.AppendLine("");

    s.AppendLine("# Start build cache");
    s.AppendLine("COPY \"MediaInAction.sln\" \"MediaInAction.sln\"");
    s.AppendLine("");

    s.AppendLine("WORKDIR /work/src");
    s.AppendLine("COPY \"src/\" \"src/\" ./"  );
    
    s.AppendLine("WORKDIR /work/tools");
    s.AppendLine("COPY \"tools/\" \"tools/\" ./"  );
    s.AppendLine("# End build cache");

    s.AppendLine("");
    s.AppendLine("WORKDIR /work");
    s.AppendLine("RUN dotnet restore \"MediaInAction.sln\"");

    var myProject = "";
    if (type == "gateways")
    {
        myProject = "MediaInAction." + u;
    }
    else if (type == "services")
    {
        myProject = "MediaInAction." + u + "Service.HttpApi.Host";
    }
    
    var thirdLevel = u.ToLower();
    s.AppendLine("COPY . .");

    if (type == "gateways")
    {
        s.AppendLine($"WORKDIR /work/src/{t}/{myProject}");
    }
    else
    {
        s.AppendLine($"WORKDIR /work/src/{t}/{thirdLevel}/{myProject}");
    }
    s.AppendLine("RUN dotnet publish  -c Release -o /app");
    s.AppendLine("");

    if (t == "services")
    {
        projectName = "MediaInAction." + name + "Service.Application.Tests" ;
        s.AppendLine("FROM build as app_tests");
        s.AppendLine($"WORKDIR /work/src/{t}/{thirdLevel}/{projectName}");
        s.AppendLine("");

        projectName = "MediaInAction." + name + "Service.Domain.Tests" ;
        s.AppendLine("FROM build as domain_tests");
        s.AppendLine($"WORKDIR /work/src/{t}/{thirdLevel}/{projectName}");
        s.AppendLine("");  
        
        if ((name == "Identity") || (name == "Video")  )
        {
            projectName = "MediaInAction." + name + "Service.EntityFrameworkCore.Tests" ;
            s.AppendLine("FROM build as db_tests");
            s.AppendLine($"WORKDIR /work/src/{t}/{thirdLevel}/{projectName}");
            s.AppendLine("");  
        }
        else if  ((name == "File") || (name == "Emby") || (name == "Trakt") || (name == "Deluge") )
        {
            projectName = "MediaInAction." + name + "Service.MongoDb.Tests" ;
            s.AppendLine("FROM build as db_tests");
            s.AppendLine($"WORKDIR /work/src/{t}/{thirdLevel}/{projectName}");
            s.AppendLine("");  
        }
   
    }
    
    s.AppendLine("FROM build AS publish");
    s.AppendLine("");

    s.AppendLine("FROM base AS final");
    s.AppendLine("WORKDIR /app");
    s.AppendLine("COPY --from=publish /app .");
    s.AppendLine($"ENTRYPOINT [\"dotnet\", \"{myProject}.dll\"]");
    return s.ToString();
}

void SaveDockerFile(string type, string name, string content)
{
    string dir = Directory.GetParent(Environment.CurrentDirectory)
        .Parent // bin
        .FullName;

    var nameLower = name.ToLower();
    if (type == "services")
    {
        var nameUpper = "MediaInAction." + name + "Service.HttpApi.Host";

        var file = dir + $"/src/{type}/{nameLower}/{nameUpper}/Dockerfile";
        File.WriteAllText(file, content);
        Console.WriteLine($"written to {file}");
    }
    else
    {
        var nameUpper = "MediaInAction." + name ;
        var file = dir + $"/src/{type}/{nameUpper}/Dockerfile";
        File.WriteAllText(file, content);
        Console.WriteLine($"written to {file}");
    }
}


void Generate(string[] names, string type)
{
    foreach (var name in names)
    {
        var dockerfile = GenerateDockerfile(type, name);
        SaveDockerFile(type, name, dockerfile);
    }
}

//Generate(frontends, "frontends");
Generate(gateways, "gateways");
Generate(services, "services");
