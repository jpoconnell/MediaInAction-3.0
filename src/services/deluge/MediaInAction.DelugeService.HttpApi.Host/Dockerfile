FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 81

# add globalization support
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# add diagnostic tools
RUN apk add --no-cache curl

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /work

# Start build cache
COPY "MediaInAction.sln" "MediaInAction.sln"

WORKDIR /work/src
COPY "src/" "src/" ./
WORKDIR /work/tools
COPY "tools/" "tools/" ./
# End build cache

WORKDIR /work
RUN dotnet restore "MediaInAction.sln"
COPY . .
WORKDIR /work/src/services/deluge/MediaInAction.DelugeService.HttpApi.Host
RUN dotnet publish  -c Release -o /app

FROM build as app_tests
WORKDIR /work/src/services/deluge/MediaInAction.DelugeService.Application.Tests

FROM build as domain_tests
WORKDIR /work/src/services/deluge/MediaInAction.DelugeService.Domain.Tests

FROM build as db_tests
WORKDIR /work/src/services/deluge/MediaInAction.DelugeService.MongoDb.Tests

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MediaInAction.DelugeService.HttpApi.Host.dll"]
