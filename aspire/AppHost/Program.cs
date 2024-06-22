using MediaInAction.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddForwardedHeaders();

var profile = "http";

// Microservices
var administrationService = builder.AddProject<Projects.MediaInAction_AdministrationService_HttpApi_Host>("administrationService");
var identityService = builder.AddProject<Projects.MediaInAction_IdentityService_HttpApi_Host>("identityService");
var cmsKitService = builder.AddProject<Projects.MediaInAction_CmskitService_HttpApi_Host>("cmsKitService");

var videoService = builder.AddProject<Projects.MediaInAction_VideoService_HttpApi_Host>("videoService", null)
    .WithHttpEndpoint(name: "http", port: 5054, isProxied: false)
    .WithEndpoint(
        endpointName: "grpc",
        callback: static endpoint =>
        {
            endpoint.Port = 8181;
            endpoint.UriScheme = "http";
            endpoint.Transport = "http2";
            endpoint.IsProxied = false;
        }
    );



// Apps
/*
var publicWebApp = builder.AddProject<Projects.MediaInAction_PublicWeb>("public-web", "https")
    .WithExternalHttpEndpoints()
    .WithReference(videoService)
    .WithReference(webPublicGateway);
*/
builder.AddProject<Projects.MediaInAction_VideoService_HttpApi_Background_Host>("videoservice-background");

// Apps
/*
var publicWebApp = builder.AddProject<Projects.MediaInAction_PublicWeb>("public-web", "https")
    .WithExternalHttpEndpoints()
    .WithReference(videoService)
    .WithReference(webPublicGateway);
*/
builder.AddProject<Projects.MediaInAction_FileService_HttpApi_Host>("fileservice");

// Apps
/*
var publicWebApp = builder.AddProject<Projects.MediaInAction_PublicWeb>("public-web", "https")
    .WithExternalHttpEndpoints()
    .WithReference(videoService)
    .WithReference(webPublicGateway);
*/
builder.AddProject<Projects.MediaInAction_TraktService_HttpApi_Background_Host>("mediainaction-traktservice-httpapi-background-host");

// Apps
/*
var publicWebApp = builder.AddProject<Projects.MediaInAction_PublicWeb>("public-web", "https")
    .WithExternalHttpEndpoints()
    .WithReference(videoService)
    .WithReference(webPublicGateway);
*/
builder.AddProject<Projects.MediaInAction_FileService_HttpApi_Background_Host>("mediainaction-fileservice-httpapi-background-host");

// Apps
/*
var publicWebApp = builder.AddProject<Projects.MediaInAction_PublicWeb>("public-web", "https")
    .WithExternalHttpEndpoints()
    .WithReference(videoService)
    .WithReference(webPublicGateway);
*/
builder.AddProject<Projects.MediaInAction_EmbyService_HttpApi_Host>("embyservice");

// Apps
/*
var publicWebApp = builder.AddProject<Projects.MediaInAction_PublicWeb>("public-web", "https")
    .WithExternalHttpEndpoints()
    .WithReference(videoService)
    .WithReference(webPublicGateway);
*/
builder.AddProject<Projects.MediaInAction_DelugeService_HttpApi_Background_Host>("delugeservice-background");

// Apps
/*
var publicWebApp = builder.AddProject<Projects.MediaInAction_PublicWeb>("public-web", "https")
    .WithExternalHttpEndpoints()
    .WithReference(videoService)
    .WithReference(webPublicGateway);
*/
builder.AddProject<Projects.MediaInAction_DelugeService_HttpApi_Host>("delugeservice");

// Apps
/*
var publicWebApp = builder.AddProject<Projects.MediaInAction_PublicWeb>("public-web", "https")
    .WithExternalHttpEndpoints()
    .WithReference(videoService)
    .WithReference(webPublicGateway);
*/
builder.Build().Run();