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

COPY "tests/integration/SystemTests/SystemTests.csproj" "tests/integration/SystemTests/SystemTests.csproj"

COPY "src/libraries/EmbyClient.Dotnet/EmbyClient.Dotnet.csproj" "src/libraries/EmbyClient.Dotnet/EmbyClient.Dotnet.csproj"

COPY "src/libraries/DelugeRPCClient.Net/DelugeRPCClient.Net.csproj" "src/libraries/DelugeRPCClient.Net/DelugeRPCClient.Net.csproj"

COPY "src/common/MediaInAction.Shared.Integration/MediaInAction.Shared.Integration.csproj" "src/common/MediaInAction.Shared.Integration/MediaInAction.Shared.Integration.csproj"

COPY "src/common/MediaInAction.Shared.HealthChecks/MediaInAction.Shared.HealthChecks.csproj" "src/common/MediaInAction.Shared.HealthChecks/MediaInAction.Shared.HealthChecks.csproj"

COPY "src/common/MediaInAction.Keycloak.DbMigrator/MediaInAction.Keycloak.DbMigrator.csproj" "src/common/MediaInAction.Keycloak.DbMigrator/MediaInAction.Keycloak.DbMigrator.csproj"

COPY "src/common/MediaInAction.Shared.Domain/MediaInAction.Shared.Domain.csproj" "src/common/MediaInAction.Shared.Domain/MediaInAction.Shared.Domain.csproj"

COPY "src/common/MediaInAction.Shared.Hosting.Microservices/MediaInAction.Shared.Hosting.Microservices.csproj" "src/common/MediaInAction.Shared.Hosting.Microservices/MediaInAction.Shared.Hosting.Microservices.csproj"

COPY "src/common/MediaInAction.Shared.Hosting.AspNetCore/MediaInAction.Shared.Hosting.AspNetCore.csproj" "src/common/MediaInAction.Shared.Hosting.AspNetCore/MediaInAction.Shared.Hosting.AspNetCore.csproj"

COPY "src/common/MediaInAction.Shared.Hosting/MediaInAction.Shared.Hosting.csproj" "src/common/MediaInAction.Shared.Hosting/MediaInAction.Shared.Hosting.csproj"

COPY "src/common/MediaInAction.Shared.Helpers/MediaInAction.Shared.Helpers.csproj" "src/common/MediaInAction.Shared.Helpers/MediaInAction.Shared.Helpers.csproj"

COPY "src/common/MediaInAction.Shared.Hosting.Gateways/MediaInAction.Shared.Hosting.Gateways.csproj" "src/common/MediaInAction.Shared.Hosting.Gateways/MediaInAction.Shared.Hosting.Gateways.csproj"

COPY "src/common/MediaInAction.Shared.Api/MediaInAction.Shared.Api.csproj" "src/common/MediaInAction.Shared.Api/MediaInAction.Shared.Api.csproj"

COPY "src/common/MediaInAction.Shared.Localization/MediaInAction.Shared.Localization.csproj" "src/common/MediaInAction.Shared.Localization/MediaInAction.Shared.Localization.csproj"

COPY "src/gateways/MediaInAction.WebPublicGateway/MediaInAction.WebPublicGateway.csproj" "src/gateways/MediaInAction.WebPublicGateway/MediaInAction.WebPublicGateway.csproj"

COPY "src/gateways/MediaInAction.WebGateway/MediaInAction.WebGateway.csproj" "src/gateways/MediaInAction.WebGateway/MediaInAction.WebGateway.csproj"

COPY "src/services/MediaInAction.VideoService.HttpApi/MediaInAction.VideoService.HttpApi.csproj" "src/services/MediaInAction.VideoService.HttpApi/MediaInAction.VideoService.HttpApi.csproj"
COPY "src/services/MediaInAction.VideoService.UnitTests/MediaInAction.VideoService.UnitTests.csproj" "src/services/MediaInAction.VideoService.HttpApi.UnitTests/MediaInAction.VideoService.HttpApi.UnitTests.csproj"
COPY "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj" "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.VideoService.HttpApi.Host/MediaInAction.VideoService.HttpApi.Host.csproj" "src/services/MediaInAction.VideoService.HttpApi.Host/MediaInAction.VideoService.HttpApi.Host.csproj"
COPY "src/services/MediaInAction.VideoService.HttpApi.UnitTests/MediaInAction.VideoService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.VideoService.HttpApi.Host.UnitTests/MediaInAction.VideoService.HttpApi.Host.UnitTests.csproj"
COPY "src/services/MediaInAction.VideoService.HttpApi.FunctionalTests/MediaInAction.VideoService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.VideoService.HttpApi.FunctionalTests/MediaInAction.VideoService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.VideoService.BackgroundWorkers/MediaInAction.VideoService.BackgroundWorkers.csproj" "src/services/MediaInAction.VideoService.BackgroundWorkers/MediaInAction.VideoService.BackgroundWorkers.csproj"
COPY "src/services/MediaInAction.VideoService.UnitTests/MediaInAction.VideoService.UnitTests.csproj" "src/services/MediaInAction.VideoService.BackgroundWorkers.UnitTests/MediaInAction.VideoService.BackgroundWorkers.UnitTests.csproj"
COPY "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj" "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.VideoService.UnitTests/MediaInAction.VideoService.UnitTests.csproj" "src/services/MediaInAction.VideoService.UnitTests/MediaInAction.VideoService.UnitTests.csproj"
COPY "src/services/MediaInAction.VideoService.UnitTests/MediaInAction.VideoService.UnitTests.csproj" "src/services/MediaInAction.VideoService.UnitTests.UnitTests/MediaInAction.VideoService.UnitTests.UnitTests.csproj"
COPY "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj" "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.VideoService.BackgroundJobs/MediaInAction.VideoService.BackgroundJobs.csproj" "src/services/MediaInAction.VideoService.BackgroundJobs/MediaInAction.VideoService.BackgroundJobs.csproj"
COPY "src/services/MediaInAction.VideoService.UnitTests/MediaInAction.VideoService.UnitTests.csproj" "src/services/MediaInAction.VideoService.BackgroundJobs.UnitTests/MediaInAction.VideoService.BackgroundJobs.UnitTests.csproj"
COPY "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj" "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.VideoService.Application.Contracts/MediaInAction.VideoService.Application.Contracts.csproj" "src/services/MediaInAction.VideoService.Application.Contracts/MediaInAction.VideoService.Application.Contracts.csproj"
COPY "src/services/MediaInAction.VideoService.Application.UnitTests/MediaInAction.VideoService.Application.UnitTests.csproj" "src/services/MediaInAction.VideoService.Application.Contracts.UnitTests/MediaInAction.VideoService.Application.Contracts.UnitTests.csproj"
COPY "src/services/MediaInAction.VideoService.Application.FunctionalTests/MediaInAction.VideoService.Application.FunctionalTests.csproj" "src/services/MediaInAction.VideoService.Application.FunctionalTests/MediaInAction.VideoService.Application.FunctionalTests.csproj"

COPY "src/services/MediaInAction.VideoService.Domain/MediaInAction.VideoService.Domain.csproj" "src/services/MediaInAction.VideoService.Domain/MediaInAction.VideoService.Domain.csproj"
COPY "src/services/MediaInAction.VideoService.UnitTests/MediaInAction.VideoService.UnitTests.csproj" "src/services/MediaInAction.VideoService.Domain.UnitTests/MediaInAction.VideoService.Domain.UnitTests.csproj"
COPY "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj" "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.VideoService.HttpApi.Client.ConsoleTestApp/MediaInAction.VideoService.HttpApi.Client.ConsoleTestApp.csproj" "src/services/MediaInAction.VideoService.HttpApi.Client.ConsoleTestApp/MediaInAction.VideoService.HttpApi.Client.ConsoleTestApp.csproj"
COPY "src/services/MediaInAction.VideoService.HttpApi.Client.UnitTests/MediaInAction.VideoService.HttpApi.Client.UnitTests.csproj" "src/services/MediaInAction.VideoService.HttpApi.Client.ConsoleTestApp.UnitTests/MediaInAction.VideoService.HttpApi.Client.ConsoleTestApp.UnitTests.csproj"
COPY "src/services/MediaInAction.VideoService.HttpApi.Client.FunctionalTests/MediaInAction.VideoService.HttpApi.Client.FunctionalTests.csproj" "src/services/MediaInAction.VideoService.HttpApi.Client.FunctionalTests/MediaInAction.VideoService.HttpApi.Client.FunctionalTests.csproj"

COPY "src/services/MediaInAction.VideoService.Domain.Shared/MediaInAction.VideoService.Domain.Shared.csproj" "src/services/MediaInAction.VideoService.Domain.Shared/MediaInAction.VideoService.Domain.Shared.csproj"
COPY "src/services/MediaInAction.VideoService.Domain.UnitTests/MediaInAction.VideoService.Domain.UnitTests.csproj" "src/services/MediaInAction.VideoService.Domain.Shared.UnitTests/MediaInAction.VideoService.Domain.Shared.UnitTests.csproj"
COPY "src/services/MediaInAction.VideoService.Domain.FunctionalTests/MediaInAction.VideoService.Domain.FunctionalTests.csproj" "src/services/MediaInAction.VideoService.Domain.FunctionalTests/MediaInAction.VideoService.Domain.FunctionalTests.csproj"

COPY "src/services/MediaInAction.VideoService.EntityFrameworkCore.Tests/MediaInAction.VideoService.EntityFrameworkCore.Tests.csproj" "src/services/MediaInAction.VideoService.EntityFrameworkCore.Tests/MediaInAction.VideoService.EntityFrameworkCore.Tests.csproj"
COPY "src/services/MediaInAction.VideoService.EntityFrameworkCore.UnitTests/MediaInAction.VideoService.EntityFrameworkCore.UnitTests.csproj" "src/services/MediaInAction.VideoService.EntityFrameworkCore.Tests.UnitTests/MediaInAction.VideoService.EntityFrameworkCore.Tests.UnitTests.csproj"
COPY "src/services/MediaInAction.VideoService.EntityFrameworkCore.FunctionalTests/MediaInAction.VideoService.EntityFrameworkCore.FunctionalTests.csproj" "src/services/MediaInAction.VideoService.EntityFrameworkCore.FunctionalTests/MediaInAction.VideoService.EntityFrameworkCore.FunctionalTests.csproj"

COPY "src/services/MediaInAction.VideoService.EntityFrameworkCore/MediaInAction.VideoService.EntityFrameworkCore.csproj" "src/services/MediaInAction.VideoService.EntityFrameworkCore/MediaInAction.VideoService.EntityFrameworkCore.csproj"
COPY "src/services/MediaInAction.VideoService.UnitTests/MediaInAction.VideoService.UnitTests.csproj" "src/services/MediaInAction.VideoService.EntityFrameworkCore.UnitTests/MediaInAction.VideoService.EntityFrameworkCore.UnitTests.csproj"
COPY "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj" "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.VideoService.TestBase/MediaInAction.VideoService.TestBase.csproj" "src/services/MediaInAction.VideoService.TestBase/MediaInAction.VideoService.TestBase.csproj"
COPY "src/services/MediaInAction.VideoService.UnitTests/MediaInAction.VideoService.UnitTests.csproj" "src/services/MediaInAction.VideoService.TestBase.UnitTests/MediaInAction.VideoService.TestBase.UnitTests.csproj"
COPY "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj" "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.VideoService.Lib/MediaInAction.VideoService.Lib.csproj" "src/services/MediaInAction.VideoService.Lib/MediaInAction.VideoService.Lib.csproj"
COPY "src/services/MediaInAction.VideoService.UnitTests/MediaInAction.VideoService.UnitTests.csproj" "src/services/MediaInAction.VideoService.Lib.UnitTests/MediaInAction.VideoService.Lib.UnitTests.csproj"
COPY "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj" "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.VideoService.HttpApi.Client/MediaInAction.VideoService.HttpApi.Client.csproj" "src/services/MediaInAction.VideoService.HttpApi.Client/MediaInAction.VideoService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.VideoService.HttpApi.UnitTests/MediaInAction.VideoService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.VideoService.HttpApi.Client.UnitTests/MediaInAction.VideoService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.VideoService.HttpApi.FunctionalTests/MediaInAction.VideoService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.VideoService.HttpApi.FunctionalTests/MediaInAction.VideoService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj" "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj"
COPY "src/services/MediaInAction.VideoService.UnitTests/MediaInAction.VideoService.UnitTests.csproj" "src/services/MediaInAction.VideoService.FunctionalTests.UnitTests/MediaInAction.VideoService.FunctionalTests.UnitTests.csproj"
COPY "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj" "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.VideoService.Application/MediaInAction.VideoService.Application.csproj" "src/services/MediaInAction.VideoService.Application/MediaInAction.VideoService.Application.csproj"
COPY "src/services/MediaInAction.VideoService.UnitTests/MediaInAction.VideoService.UnitTests.csproj" "src/services/MediaInAction.VideoService.Application.UnitTests/MediaInAction.VideoService.Application.UnitTests.csproj"
COPY "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj" "src/services/MediaInAction.VideoService.FunctionalTests/MediaInAction.VideoService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.IdentityService.Application.Contracts/MediaInAction.IdentityService.Application.Contracts.csproj" "src/services/MediaInAction.IdentityService.Application.Contracts/MediaInAction.IdentityService.Application.Contracts.csproj"
COPY "src/services/MediaInAction.IdentityService.Application.UnitTests/MediaInAction.IdentityService.Application.UnitTests.csproj" "src/services/MediaInAction.IdentityService.Application.Contracts.UnitTests/MediaInAction.IdentityService.Application.Contracts.UnitTests.csproj"
COPY "src/services/MediaInAction.IdentityService.Application.FunctionalTests/MediaInAction.IdentityService.Application.FunctionalTests.csproj" "src/services/MediaInAction.IdentityService.Application.FunctionalTests/MediaInAction.IdentityService.Application.FunctionalTests.csproj"

COPY "src/services/MediaInAction.IdentityService.Application/MediaInAction.IdentityService.Application.csproj" "src/services/MediaInAction.IdentityService.Application/MediaInAction.IdentityService.Application.csproj"
COPY "src/services/MediaInAction.IdentityService.UnitTests/MediaInAction.IdentityService.UnitTests.csproj" "src/services/MediaInAction.IdentityService.Application.UnitTests/MediaInAction.IdentityService.Application.UnitTests.csproj"
COPY "src/services/MediaInAction.IdentityService.FunctionalTests/MediaInAction.IdentityService.FunctionalTests.csproj" "src/services/MediaInAction.IdentityService.FunctionalTests/MediaInAction.IdentityService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.IdentityService.Domain/MediaInAction.IdentityService.Domain.csproj" "src/services/MediaInAction.IdentityService.Domain/MediaInAction.IdentityService.Domain.csproj"
COPY "src/services/MediaInAction.IdentityService.UnitTests/MediaInAction.IdentityService.UnitTests.csproj" "src/services/MediaInAction.IdentityService.Domain.UnitTests/MediaInAction.IdentityService.Domain.UnitTests.csproj"
COPY "src/services/MediaInAction.IdentityService.FunctionalTests/MediaInAction.IdentityService.FunctionalTests.csproj" "src/services/MediaInAction.IdentityService.FunctionalTests/MediaInAction.IdentityService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.IdentityService.EntityFrameworkCore/MediaInAction.IdentityService.EntityFrameworkCore.csproj" "src/services/MediaInAction.IdentityService.EntityFrameworkCore/MediaInAction.IdentityService.EntityFrameworkCore.csproj"
COPY "src/services/MediaInAction.IdentityService.UnitTests/MediaInAction.IdentityService.UnitTests.csproj" "src/services/MediaInAction.IdentityService.EntityFrameworkCore.UnitTests/MediaInAction.IdentityService.EntityFrameworkCore.UnitTests.csproj"
COPY "src/services/MediaInAction.IdentityService.FunctionalTests/MediaInAction.IdentityService.FunctionalTests.csproj" "src/services/MediaInAction.IdentityService.FunctionalTests/MediaInAction.IdentityService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.IdentityService.UnitTests/MediaInAction.IdentityService.UnitTests.csproj" "src/services/MediaInAction.IdentityService.UnitTests/MediaInAction.IdentityService.UnitTests.csproj"
COPY "src/services/MediaInAction.IdentityService.UnitTests/MediaInAction.IdentityService.UnitTests.csproj" "src/services/MediaInAction.IdentityService.UnitTests.UnitTests/MediaInAction.IdentityService.UnitTests.UnitTests.csproj"
COPY "src/services/MediaInAction.IdentityService.FunctionalTests/MediaInAction.IdentityService.FunctionalTests.csproj" "src/services/MediaInAction.IdentityService.FunctionalTests/MediaInAction.IdentityService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.IdentityService.FunctionalTests/MediaInAction.IdentityService.FunctionalTests.csproj" "src/services/MediaInAction.IdentityService.FunctionalTests/MediaInAction.IdentityService.FunctionalTests.csproj"
COPY "src/services/MediaInAction.IdentityService.UnitTests/MediaInAction.IdentityService.UnitTests.csproj" "src/services/MediaInAction.IdentityService.FunctionalTests.UnitTests/MediaInAction.IdentityService.FunctionalTests.UnitTests.csproj"
COPY "src/services/MediaInAction.IdentityService.FunctionalTests/MediaInAction.IdentityService.FunctionalTests.csproj" "src/services/MediaInAction.IdentityService.FunctionalTests/MediaInAction.IdentityService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.IdentityService.EntityFrameworkCore.Tests/MediaInAction.IdentityService.EntityFrameworkCore.Tests.csproj" "src/services/MediaInAction.IdentityService.EntityFrameworkCore.Tests/MediaInAction.IdentityService.EntityFrameworkCore.Tests.csproj"
COPY "src/services/MediaInAction.IdentityService.EntityFrameworkCore.UnitTests/MediaInAction.IdentityService.EntityFrameworkCore.UnitTests.csproj" "src/services/MediaInAction.IdentityService.EntityFrameworkCore.Tests.UnitTests/MediaInAction.IdentityService.EntityFrameworkCore.Tests.UnitTests.csproj"
COPY "src/services/MediaInAction.IdentityService.EntityFrameworkCore.FunctionalTests/MediaInAction.IdentityService.EntityFrameworkCore.FunctionalTests.csproj" "src/services/MediaInAction.IdentityService.EntityFrameworkCore.FunctionalTests/MediaInAction.IdentityService.EntityFrameworkCore.FunctionalTests.csproj"

COPY "src/services/MediaInAction.IdentityService.HttpApi/MediaInAction.IdentityService.HttpApi.csproj" "src/services/MediaInAction.IdentityService.HttpApi/MediaInAction.IdentityService.HttpApi.csproj"
COPY "src/services/MediaInAction.IdentityService.UnitTests/MediaInAction.IdentityService.UnitTests.csproj" "src/services/MediaInAction.IdentityService.HttpApi.UnitTests/MediaInAction.IdentityService.HttpApi.UnitTests.csproj"
COPY "src/services/MediaInAction.IdentityService.FunctionalTests/MediaInAction.IdentityService.FunctionalTests.csproj" "src/services/MediaInAction.IdentityService.FunctionalTests/MediaInAction.IdentityService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.IdentityService.TestBase/MediaInAction.IdentityService.TestBase.csproj" "src/services/MediaInAction.IdentityService.TestBase/MediaInAction.IdentityService.TestBase.csproj"
COPY "src/services/MediaInAction.IdentityService.UnitTests/MediaInAction.IdentityService.UnitTests.csproj" "src/services/MediaInAction.IdentityService.TestBase.UnitTests/MediaInAction.IdentityService.TestBase.UnitTests.csproj"
COPY "src/services/MediaInAction.IdentityService.FunctionalTests/MediaInAction.IdentityService.FunctionalTests.csproj" "src/services/MediaInAction.IdentityService.FunctionalTests/MediaInAction.IdentityService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.IdentityService.Domain.Shared/MediaInAction.IdentityService.Domain.Shared.csproj" "src/services/MediaInAction.IdentityService.Domain.Shared/MediaInAction.IdentityService.Domain.Shared.csproj"
COPY "src/services/MediaInAction.IdentityService.Domain.UnitTests/MediaInAction.IdentityService.Domain.UnitTests.csproj" "src/services/MediaInAction.IdentityService.Domain.Shared.UnitTests/MediaInAction.IdentityService.Domain.Shared.UnitTests.csproj"
COPY "src/services/MediaInAction.IdentityService.Domain.FunctionalTests/MediaInAction.IdentityService.Domain.FunctionalTests.csproj" "src/services/MediaInAction.IdentityService.Domain.FunctionalTests/MediaInAction.IdentityService.Domain.FunctionalTests.csproj"

COPY "src/services/MediaInAction.IdentityService.HttpApi.Host/MediaInAction.IdentityService.HttpApi.Host.csproj" "src/services/MediaInAction.IdentityService.HttpApi.Host/MediaInAction.IdentityService.HttpApi.Host.csproj"
COPY "src/services/MediaInAction.IdentityService.HttpApi.UnitTests/MediaInAction.IdentityService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.IdentityService.HttpApi.Host.UnitTests/MediaInAction.IdentityService.HttpApi.Host.UnitTests.csproj"
COPY "src/services/MediaInAction.IdentityService.HttpApi.FunctionalTests/MediaInAction.IdentityService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.IdentityService.HttpApi.FunctionalTests/MediaInAction.IdentityService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.IdentityService.HttpApi.Client/MediaInAction.IdentityService.HttpApi.Client.csproj" "src/services/MediaInAction.IdentityService.HttpApi.Client/MediaInAction.IdentityService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.IdentityService.HttpApi.UnitTests/MediaInAction.IdentityService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.IdentityService.HttpApi.Client.UnitTests/MediaInAction.IdentityService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.IdentityService.HttpApi.FunctionalTests/MediaInAction.IdentityService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.IdentityService.HttpApi.FunctionalTests/MediaInAction.IdentityService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj" "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj"
COPY "src/services/MediaInAction.FileService.UnitTests/MediaInAction.FileService.UnitTests.csproj" "src/services/MediaInAction.FileService.FunctionalTests.UnitTests/MediaInAction.FileService.FunctionalTests.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj" "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.BackgroundWorkers/MediaInAction.FileService.BackgroundWorkers.csproj" "src/services/MediaInAction.FileService.BackgroundWorkers/MediaInAction.FileService.BackgroundWorkers.csproj"
COPY "src/services/MediaInAction.FileService.UnitTests/MediaInAction.FileService.UnitTests.csproj" "src/services/MediaInAction.FileService.BackgroundWorkers.UnitTests/MediaInAction.FileService.BackgroundWorkers.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj" "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.UnitTests/MediaInAction.FileService.UnitTests.csproj" "src/services/MediaInAction.FileService.UnitTests/MediaInAction.FileService.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.UnitTests/MediaInAction.FileService.UnitTests.csproj" "src/services/MediaInAction.FileService.UnitTests.UnitTests/MediaInAction.FileService.UnitTests.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj" "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Host/MediaInAction.FileService.HttpApi.Host.csproj" "src/services/MediaInAction.FileService.HttpApi.Host/MediaInAction.FileService.HttpApi.Host.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Host.UnitTests/MediaInAction.FileService.HttpApi.Host.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.Domain/MediaInAction.FileService.Domain.csproj" "src/services/MediaInAction.FileService.Domain/MediaInAction.FileService.Domain.csproj"
COPY "src/services/MediaInAction.FileService.UnitTests/MediaInAction.FileService.UnitTests.csproj" "src/services/MediaInAction.FileService.Domain.UnitTests/MediaInAction.FileService.Domain.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj" "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.Application/MediaInAction.FileService.Application.csproj" "src/services/MediaInAction.FileService.Application/MediaInAction.FileService.Application.csproj"
COPY "src/services/MediaInAction.FileService.UnitTests/MediaInAction.FileService.UnitTests.csproj" "src/services/MediaInAction.FileService.Application.UnitTests/MediaInAction.FileService.Application.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj" "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.Application.Contracts/MediaInAction.FileService.Application.Contracts.csproj" "src/services/MediaInAction.FileService.Application.Contracts/MediaInAction.FileService.Application.Contracts.csproj"
COPY "src/services/MediaInAction.FileService.Application.UnitTests/MediaInAction.FileService.Application.UnitTests.csproj" "src/services/MediaInAction.FileService.Application.Contracts.UnitTests/MediaInAction.FileService.Application.Contracts.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.Application.FunctionalTests/MediaInAction.FileService.Application.FunctionalTests.csproj" "src/services/MediaInAction.FileService.Application.FunctionalTests/MediaInAction.FileService.Application.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.Lib/MediaInAction.FileService.Lib.csproj" "src/services/MediaInAction.FileService.Lib/MediaInAction.FileService.Lib.csproj"
COPY "src/services/MediaInAction.FileService.UnitTests/MediaInAction.FileService.UnitTests.csproj" "src/services/MediaInAction.FileService.Lib.UnitTests/MediaInAction.FileService.Lib.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj" "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.MongoDb/MediaInAction.FileService.MongoDb.csproj" "src/services/MediaInAction.FileService.MongoDb/MediaInAction.FileService.MongoDb.csproj"
COPY "src/services/MediaInAction.FileService.UnitTests/MediaInAction.FileService.UnitTests.csproj" "src/services/MediaInAction.FileService.MongoDb.UnitTests/MediaInAction.FileService.MongoDb.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj" "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi/MediaInAction.FileService.HttpApi.csproj" "src/services/MediaInAction.FileService.HttpApi/MediaInAction.FileService.HttpApi.csproj"
COPY "src/services/MediaInAction.FileService.UnitTests/MediaInAction.FileService.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj" "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.Domain.Shared/MediaInAction.FileService.Domain.Shared.csproj" "src/services/MediaInAction.FileService.Domain.Shared/MediaInAction.FileService.Domain.Shared.csproj"
COPY "src/services/MediaInAction.FileService.Domain.UnitTests/MediaInAction.FileService.Domain.UnitTests.csproj" "src/services/MediaInAction.FileService.Domain.Shared.UnitTests/MediaInAction.FileService.Domain.Shared.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.Domain.FunctionalTests/MediaInAction.FileService.Domain.FunctionalTests.csproj" "src/services/MediaInAction.FileService.Domain.FunctionalTests/MediaInAction.FileService.Domain.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.TestBase/MediaInAction.FileService.TestBase.csproj" "src/services/MediaInAction.FileService.TestBase/MediaInAction.FileService.TestBase.csproj"
COPY "src/services/MediaInAction.FileService.UnitTests/MediaInAction.FileService.UnitTests.csproj" "src/services/MediaInAction.FileService.TestBase.UnitTests/MediaInAction.FileService.TestBase.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj" "src/services/MediaInAction.FileService.FunctionalTests/MediaInAction.FileService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.CmskitService.HttpApi.Host/MediaInAction.CmskitService.HttpApi.Host.csproj" "src/services/MediaInAction.CmskitService.HttpApi.Host/MediaInAction.CmskitService.HttpApi.Host.csproj"
COPY "src/services/MediaInAction.CmskitService.HttpApi.UnitTests/MediaInAction.CmskitService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.CmskitService.HttpApi.Host.UnitTests/MediaInAction.CmskitService.HttpApi.Host.UnitTests.csproj"
COPY "src/services/MediaInAction.CmskitService.HttpApi.FunctionalTests/MediaInAction.CmskitService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.CmskitService.HttpApi.FunctionalTests/MediaInAction.CmskitService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.CmskitService.Domain/MediaInAction.CmskitService.Domain.csproj" "src/services/MediaInAction.CmskitService.Domain/MediaInAction.CmskitService.Domain.csproj"
COPY "src/services/MediaInAction.CmskitService.UnitTests/MediaInAction.CmskitService.UnitTests.csproj" "src/services/MediaInAction.CmskitService.Domain.UnitTests/MediaInAction.CmskitService.Domain.UnitTests.csproj"
COPY "src/services/MediaInAction.CmskitService.FunctionalTests/MediaInAction.CmskitService.FunctionalTests.csproj" "src/services/MediaInAction.CmskitService.FunctionalTests/MediaInAction.CmskitService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.CmskitService.Application/MediaInAction.CmskitService.Application.csproj" "src/services/MediaInAction.CmskitService.Application/MediaInAction.CmskitService.Application.csproj"
COPY "src/services/MediaInAction.CmskitService.UnitTests/MediaInAction.CmskitService.UnitTests.csproj" "src/services/MediaInAction.CmskitService.Application.UnitTests/MediaInAction.CmskitService.Application.UnitTests.csproj"
COPY "src/services/MediaInAction.CmskitService.FunctionalTests/MediaInAction.CmskitService.FunctionalTests.csproj" "src/services/MediaInAction.CmskitService.FunctionalTests/MediaInAction.CmskitService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.CmskitService.FunctionalTests/MediaInAction.CmskitService.FunctionalTests.csproj" "src/services/MediaInAction.CmskitService.FunctionalTests/MediaInAction.CmskitService.FunctionalTests.csproj"
COPY "src/services/MediaInAction.CmskitService.UnitTests/MediaInAction.CmskitService.UnitTests.csproj" "src/services/MediaInAction.CmskitService.FunctionalTests.UnitTests/MediaInAction.CmskitService.FunctionalTests.UnitTests.csproj"
COPY "src/services/MediaInAction.CmskitService.FunctionalTests/MediaInAction.CmskitService.FunctionalTests.csproj" "src/services/MediaInAction.CmskitService.FunctionalTests/MediaInAction.CmskitService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.CmskitService.EntityFrameworkCore/MediaInAction.CmskitService.EntityFrameworkCore.csproj" "src/services/MediaInAction.CmskitService.EntityFrameworkCore/MediaInAction.CmskitService.EntityFrameworkCore.csproj"
COPY "src/services/MediaInAction.CmskitService.UnitTests/MediaInAction.CmskitService.UnitTests.csproj" "src/services/MediaInAction.CmskitService.EntityFrameworkCore.UnitTests/MediaInAction.CmskitService.EntityFrameworkCore.UnitTests.csproj"
COPY "src/services/MediaInAction.CmskitService.FunctionalTests/MediaInAction.CmskitService.FunctionalTests.csproj" "src/services/MediaInAction.CmskitService.FunctionalTests/MediaInAction.CmskitService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.CmskitService.TestBase/MediaInAction.CmskitService.TestBase.csproj" "src/services/MediaInAction.CmskitService.TestBase/MediaInAction.CmskitService.TestBase.csproj"
COPY "src/services/MediaInAction.CmskitService.UnitTests/MediaInAction.CmskitService.UnitTests.csproj" "src/services/MediaInAction.CmskitService.TestBase.UnitTests/MediaInAction.CmskitService.TestBase.UnitTests.csproj"
COPY "src/services/MediaInAction.CmskitService.FunctionalTests/MediaInAction.CmskitService.FunctionalTests.csproj" "src/services/MediaInAction.CmskitService.FunctionalTests/MediaInAction.CmskitService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.CmskitService.Application.Contracts/MediaInAction.CmskitService.Application.Contracts.csproj" "src/services/MediaInAction.CmskitService.Application.Contracts/MediaInAction.CmskitService.Application.Contracts.csproj"
COPY "src/services/MediaInAction.CmskitService.Application.UnitTests/MediaInAction.CmskitService.Application.UnitTests.csproj" "src/services/MediaInAction.CmskitService.Application.Contracts.UnitTests/MediaInAction.CmskitService.Application.Contracts.UnitTests.csproj"
COPY "src/services/MediaInAction.CmskitService.Application.FunctionalTests/MediaInAction.CmskitService.Application.FunctionalTests.csproj" "src/services/MediaInAction.CmskitService.Application.FunctionalTests/MediaInAction.CmskitService.Application.FunctionalTests.csproj"

COPY "src/services/MediaInAction.CmskitService.HttpApi/MediaInAction.CmskitService.HttpApi.csproj" "src/services/MediaInAction.CmskitService.HttpApi/MediaInAction.CmskitService.HttpApi.csproj"
COPY "src/services/MediaInAction.CmskitService.UnitTests/MediaInAction.CmskitService.UnitTests.csproj" "src/services/MediaInAction.CmskitService.HttpApi.UnitTests/MediaInAction.CmskitService.HttpApi.UnitTests.csproj"
COPY "src/services/MediaInAction.CmskitService.FunctionalTests/MediaInAction.CmskitService.FunctionalTests.csproj" "src/services/MediaInAction.CmskitService.FunctionalTests/MediaInAction.CmskitService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.CmskitService.Domain.Shared/MediaInAction.CmskitService.Domain.Shared.csproj" "src/services/MediaInAction.CmskitService.Domain.Shared/MediaInAction.CmskitService.Domain.Shared.csproj"
COPY "src/services/MediaInAction.CmskitService.Domain.UnitTests/MediaInAction.CmskitService.Domain.UnitTests.csproj" "src/services/MediaInAction.CmskitService.Domain.Shared.UnitTests/MediaInAction.CmskitService.Domain.Shared.UnitTests.csproj"
COPY "src/services/MediaInAction.CmskitService.Domain.FunctionalTests/MediaInAction.CmskitService.Domain.FunctionalTests.csproj" "src/services/MediaInAction.CmskitService.Domain.FunctionalTests/MediaInAction.CmskitService.Domain.FunctionalTests.csproj"

COPY "src/services/MediaInAction.CmskitService.EntityFrameworkCore.Tests/MediaInAction.CmskitService.EntityFrameworkCore.Tests.csproj" "src/services/MediaInAction.CmskitService.EntityFrameworkCore.Tests/MediaInAction.CmskitService.EntityFrameworkCore.Tests.csproj"
COPY "src/services/MediaInAction.CmskitService.EntityFrameworkCore.UnitTests/MediaInAction.CmskitService.EntityFrameworkCore.UnitTests.csproj" "src/services/MediaInAction.CmskitService.EntityFrameworkCore.Tests.UnitTests/MediaInAction.CmskitService.EntityFrameworkCore.Tests.UnitTests.csproj"
COPY "src/services/MediaInAction.CmskitService.EntityFrameworkCore.FunctionalTests/MediaInAction.CmskitService.EntityFrameworkCore.FunctionalTests.csproj" "src/services/MediaInAction.CmskitService.EntityFrameworkCore.FunctionalTests/MediaInAction.CmskitService.EntityFrameworkCore.FunctionalTests.csproj"

COPY "src/services/MediaInAction.CmskitService.UnitTests/MediaInAction.CmskitService.UnitTests.csproj" "src/services/MediaInAction.CmskitService.UnitTests/MediaInAction.CmskitService.UnitTests.csproj"
COPY "src/services/MediaInAction.CmskitService.UnitTests/MediaInAction.CmskitService.UnitTests.csproj" "src/services/MediaInAction.CmskitService.UnitTests.UnitTests/MediaInAction.CmskitService.UnitTests.UnitTests.csproj"
COPY "src/services/MediaInAction.CmskitService.FunctionalTests/MediaInAction.CmskitService.FunctionalTests.csproj" "src/services/MediaInAction.CmskitService.FunctionalTests/MediaInAction.CmskitService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.CmskitService.HttpApi.Client/MediaInAction.CmskitService.HttpApi.Client.csproj" "src/services/MediaInAction.CmskitService.HttpApi.Client/MediaInAction.CmskitService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.CmskitService.HttpApi.UnitTests/MediaInAction.CmskitService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.CmskitService.HttpApi.Client.UnitTests/MediaInAction.CmskitService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.CmskitService.HttpApi.FunctionalTests/MediaInAction.CmskitService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.CmskitService.HttpApi.FunctionalTests/MediaInAction.CmskitService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.TraktService.Domain.Shared/MediaInAction.TraktService.Domain.Shared.csproj" "src/services/MediaInAction.TraktService.Domain.Shared/MediaInAction.TraktService.Domain.Shared.csproj"
COPY "src/services/MediaInAction.TraktService.Domain.UnitTests/MediaInAction.TraktService.Domain.UnitTests.csproj" "src/services/MediaInAction.TraktService.Domain.Shared.UnitTests/MediaInAction.TraktService.Domain.Shared.UnitTests.csproj"
COPY "src/services/MediaInAction.TraktService.Domain.FunctionalTests/MediaInAction.TraktService.Domain.FunctionalTests.csproj" "src/services/MediaInAction.TraktService.Domain.FunctionalTests/MediaInAction.TraktService.Domain.FunctionalTests.csproj"

COPY "src/services/MediaInAction.TraktService.UnitTests/MediaInAction.TraktService.UnitTests.csproj" "src/services/MediaInAction.TraktService.UnitTests/MediaInAction.TraktService.UnitTests.csproj"
COPY "src/services/MediaInAction.TraktService.UnitTests/MediaInAction.TraktService.UnitTests.csproj" "src/services/MediaInAction.TraktService.UnitTests.UnitTests/MediaInAction.TraktService.UnitTests.UnitTests.csproj"
COPY "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj" "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.TraktService.Application/MediaInAction.TraktService.Application.csproj" "src/services/MediaInAction.TraktService.Application/MediaInAction.TraktService.Application.csproj"
COPY "src/services/MediaInAction.TraktService.UnitTests/MediaInAction.TraktService.UnitTests.csproj" "src/services/MediaInAction.TraktService.Application.UnitTests/MediaInAction.TraktService.Application.UnitTests.csproj"
COPY "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj" "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.TraktService.HttpApi/MediaInAction.TraktService.HttpApi.csproj" "src/services/MediaInAction.TraktService.HttpApi/MediaInAction.TraktService.HttpApi.csproj"
COPY "src/services/MediaInAction.TraktService.UnitTests/MediaInAction.TraktService.UnitTests.csproj" "src/services/MediaInAction.TraktService.HttpApi.UnitTests/MediaInAction.TraktService.HttpApi.UnitTests.csproj"
COPY "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj" "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.TraktService.MongoDb/MediaInAction.TraktService.MongoDb.csproj" "src/services/MediaInAction.TraktService.MongoDb/MediaInAction.TraktService.MongoDb.csproj"
COPY "src/services/MediaInAction.TraktService.UnitTests/MediaInAction.TraktService.UnitTests.csproj" "src/services/MediaInAction.TraktService.MongoDb.UnitTests/MediaInAction.TraktService.MongoDb.UnitTests.csproj"
COPY "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj" "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj" "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj"
COPY "src/services/MediaInAction.TraktService.UnitTests/MediaInAction.TraktService.UnitTests.csproj" "src/services/MediaInAction.TraktService.FunctionalTests.UnitTests/MediaInAction.TraktService.FunctionalTests.UnitTests.csproj"
COPY "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj" "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.TraktService.TestBase/MediaInAction.TraktService.TestBase.csproj" "src/services/MediaInAction.TraktService.TestBase/MediaInAction.TraktService.TestBase.csproj"
COPY "src/services/MediaInAction.TraktService.UnitTests/MediaInAction.TraktService.UnitTests.csproj" "src/services/MediaInAction.TraktService.TestBase.UnitTests/MediaInAction.TraktService.TestBase.UnitTests.csproj"
COPY "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj" "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.TraktService.BackgroundJobs/MediaInAction.TraktService.BackgroundJobs.csproj" "src/services/MediaInAction.TraktService.BackgroundJobs/MediaInAction.TraktService.BackgroundJobs.csproj"
COPY "src/services/MediaInAction.TraktService.UnitTests/MediaInAction.TraktService.UnitTests.csproj" "src/services/MediaInAction.TraktService.BackgroundJobs.UnitTests/MediaInAction.TraktService.BackgroundJobs.UnitTests.csproj"
COPY "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj" "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.TraktService.BackgroundWorkers/MediaInAction.TraktService.BackgroundWorkers.csproj" "src/services/MediaInAction.TraktService.BackgroundWorkers/MediaInAction.TraktService.BackgroundWorkers.csproj"
COPY "src/services/MediaInAction.TraktService.UnitTests/MediaInAction.TraktService.UnitTests.csproj" "src/services/MediaInAction.TraktService.BackgroundWorkers.UnitTests/MediaInAction.TraktService.BackgroundWorkers.UnitTests.csproj"
COPY "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj" "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.TraktService.Lib/MediaInAction.TraktService.Lib.csproj" "src/services/MediaInAction.TraktService.Lib/MediaInAction.TraktService.Lib.csproj"
COPY "src/services/MediaInAction.TraktService.UnitTests/MediaInAction.TraktService.UnitTests.csproj" "src/services/MediaInAction.TraktService.Lib.UnitTests/MediaInAction.TraktService.Lib.UnitTests.csproj"
COPY "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj" "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.TraktService.HttpApi.Client/MediaInAction.TraktService.HttpApi.Client.csproj" "src/services/MediaInAction.TraktService.HttpApi.Client/MediaInAction.TraktService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.TraktService.HttpApi.UnitTests/MediaInAction.TraktService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.TraktService.HttpApi.Client.UnitTests/MediaInAction.TraktService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.TraktService.HttpApi.FunctionalTests/MediaInAction.TraktService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.TraktService.HttpApi.FunctionalTests/MediaInAction.TraktService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.TraktService.HttpApi.Host/MediaInAction.TraktService.HttpApi.Host.csproj" "src/services/MediaInAction.TraktService.HttpApi.Host/MediaInAction.TraktService.HttpApi.Host.csproj"
COPY "src/services/MediaInAction.TraktService.HttpApi.UnitTests/MediaInAction.TraktService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.TraktService.HttpApi.Host.UnitTests/MediaInAction.TraktService.HttpApi.Host.UnitTests.csproj"
COPY "src/services/MediaInAction.TraktService.HttpApi.FunctionalTests/MediaInAction.TraktService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.TraktService.HttpApi.FunctionalTests/MediaInAction.TraktService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.TraktService.Domain/MediaInAction.TraktService.Domain.csproj" "src/services/MediaInAction.TraktService.Domain/MediaInAction.TraktService.Domain.csproj"
COPY "src/services/MediaInAction.TraktService.UnitTests/MediaInAction.TraktService.UnitTests.csproj" "src/services/MediaInAction.TraktService.Domain.UnitTests/MediaInAction.TraktService.Domain.UnitTests.csproj"
COPY "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj" "src/services/MediaInAction.TraktService.FunctionalTests/MediaInAction.TraktService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.TraktService.Application.Contracts/MediaInAction.TraktService.Application.Contracts.csproj" "src/services/MediaInAction.TraktService.Application.Contracts/MediaInAction.TraktService.Application.Contracts.csproj"
COPY "src/services/MediaInAction.TraktService.Application.UnitTests/MediaInAction.TraktService.Application.UnitTests.csproj" "src/services/MediaInAction.TraktService.Application.Contracts.UnitTests/MediaInAction.TraktService.Application.Contracts.UnitTests.csproj"
COPY "src/services/MediaInAction.TraktService.Application.FunctionalTests/MediaInAction.TraktService.Application.FunctionalTests.csproj" "src/services/MediaInAction.TraktService.Application.FunctionalTests/MediaInAction.TraktService.Application.FunctionalTests.csproj"

COPY "src/services/MediaInAction.DelugeService.TestBase/MediaInAction.DelugeService.TestBase.csproj" "src/services/MediaInAction.DelugeService.TestBase/MediaInAction.DelugeService.TestBase.csproj"
COPY "src/services/MediaInAction.DelugeService.UnitTests/MediaInAction.DelugeService.UnitTests.csproj" "src/services/MediaInAction.DelugeService.TestBase.UnitTests/MediaInAction.DelugeService.TestBase.UnitTests.csproj"
COPY "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj" "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.DelugeService.HttpApi.Client.ConsoleTestApp/MediaInAction.DelugeService.HttpApi.Client.ConsoleTestApp.csproj" "src/services/MediaInAction.DelugeService.HttpApi.Client.ConsoleTestApp/MediaInAction.DelugeService.HttpApi.Client.ConsoleTestApp.csproj"
COPY "src/services/MediaInAction.DelugeService.HttpApi.Client.UnitTests/MediaInAction.DelugeService.HttpApi.Client.UnitTests.csproj" "src/services/MediaInAction.DelugeService.HttpApi.Client.ConsoleTestApp.UnitTests/MediaInAction.DelugeService.HttpApi.Client.ConsoleTestApp.UnitTests.csproj"
COPY "src/services/MediaInAction.DelugeService.HttpApi.Client.FunctionalTests/MediaInAction.DelugeService.HttpApi.Client.FunctionalTests.csproj" "src/services/MediaInAction.DelugeService.HttpApi.Client.FunctionalTests/MediaInAction.DelugeService.HttpApi.Client.FunctionalTests.csproj"

COPY "src/services/MediaInAction.DelugeService.BackgroundWorkers/MediaInAction.DelugeService.BackgroundWorkers.csproj" "src/services/MediaInAction.DelugeService.BackgroundWorkers/MediaInAction.DelugeService.BackgroundWorkers.csproj"
COPY "src/services/MediaInAction.DelugeService.UnitTests/MediaInAction.DelugeService.UnitTests.csproj" "src/services/MediaInAction.DelugeService.BackgroundWorkers.UnitTests/MediaInAction.DelugeService.BackgroundWorkers.UnitTests.csproj"
COPY "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj" "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj" "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj"
COPY "src/services/MediaInAction.DelugeService.UnitTests/MediaInAction.DelugeService.UnitTests.csproj" "src/services/MediaInAction.DelugeService.FunctionalTests.UnitTests/MediaInAction.DelugeService.FunctionalTests.UnitTests.csproj"
COPY "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj" "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.DelugeService.MongoDb/MediaInAction.DelugeService.MongoDb.csproj" "src/services/MediaInAction.DelugeService.MongoDb/MediaInAction.DelugeService.MongoDb.csproj"
COPY "src/services/MediaInAction.DelugeService.UnitTests/MediaInAction.DelugeService.UnitTests.csproj" "src/services/MediaInAction.DelugeService.MongoDb.UnitTests/MediaInAction.DelugeService.MongoDb.UnitTests.csproj"
COPY "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj" "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.DelugeService.HttpApi/MediaInAction.DelugeService.HttpApi.csproj" "src/services/MediaInAction.DelugeService.HttpApi/MediaInAction.DelugeService.HttpApi.csproj"
COPY "src/services/MediaInAction.DelugeService.UnitTests/MediaInAction.DelugeService.UnitTests.csproj" "src/services/MediaInAction.DelugeService.HttpApi.UnitTests/MediaInAction.DelugeService.HttpApi.UnitTests.csproj"
COPY "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj" "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.DelugeService.Application/MediaInAction.DelugeService.Application.csproj" "src/services/MediaInAction.DelugeService.Application/MediaInAction.DelugeService.Application.csproj"
COPY "src/services/MediaInAction.DelugeService.UnitTests/MediaInAction.DelugeService.UnitTests.csproj" "src/services/MediaInAction.DelugeService.Application.UnitTests/MediaInAction.DelugeService.Application.UnitTests.csproj"
COPY "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj" "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.DelugeService.HttpApi.Client/MediaInAction.DelugeService.HttpApi.Client.csproj" "src/services/MediaInAction.DelugeService.HttpApi.Client/MediaInAction.DelugeService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.DelugeService.HttpApi.UnitTests/MediaInAction.DelugeService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.DelugeService.HttpApi.Client.UnitTests/MediaInAction.DelugeService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.DelugeService.HttpApi.FunctionalTests/MediaInAction.DelugeService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.DelugeService.HttpApi.FunctionalTests/MediaInAction.DelugeService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.DelugeService.HttpApi.Host/MediaInAction.DelugeService.HttpApi.Host.csproj" "src/services/MediaInAction.DelugeService.HttpApi.Host/MediaInAction.DelugeService.HttpApi.Host.csproj"
COPY "src/services/MediaInAction.DelugeService.HttpApi.UnitTests/MediaInAction.DelugeService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.DelugeService.HttpApi.Host.UnitTests/MediaInAction.DelugeService.HttpApi.Host.UnitTests.csproj"
COPY "src/services/MediaInAction.DelugeService.HttpApi.FunctionalTests/MediaInAction.DelugeService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.DelugeService.HttpApi.FunctionalTests/MediaInAction.DelugeService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.DelugeService.MongoDb.Tests/MediaInAction.DelugeService.MongoDb.Tests.csproj" "src/services/MediaInAction.DelugeService.MongoDb.Tests/MediaInAction.DelugeService.MongoDb.Tests.csproj"
COPY "src/services/MediaInAction.DelugeService.MongoDb.UnitTests/MediaInAction.DelugeService.MongoDb.UnitTests.csproj" "src/services/MediaInAction.DelugeService.MongoDb.Tests.UnitTests/MediaInAction.DelugeService.MongoDb.Tests.UnitTests.csproj"
COPY "src/services/MediaInAction.DelugeService.MongoDb.FunctionalTests/MediaInAction.DelugeService.MongoDb.FunctionalTests.csproj" "src/services/MediaInAction.DelugeService.MongoDb.FunctionalTests/MediaInAction.DelugeService.MongoDb.FunctionalTests.csproj"

COPY "src/services/MediaInAction.DelugeService.UnitTests/MediaInAction.DelugeService.UnitTests.csproj" "src/services/MediaInAction.DelugeService.UnitTests/MediaInAction.DelugeService.UnitTests.csproj"
COPY "src/services/MediaInAction.DelugeService.UnitTests/MediaInAction.DelugeService.UnitTests.csproj" "src/services/MediaInAction.DelugeService.UnitTests.UnitTests/MediaInAction.DelugeService.UnitTests.UnitTests.csproj"
COPY "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj" "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.DelugeService.Lib/MediaInAction.DelugeService.Lib.csproj" "src/services/MediaInAction.DelugeService.Lib/MediaInAction.DelugeService.Lib.csproj"
COPY "src/services/MediaInAction.DelugeService.UnitTests/MediaInAction.DelugeService.UnitTests.csproj" "src/services/MediaInAction.DelugeService.Lib.UnitTests/MediaInAction.DelugeService.Lib.UnitTests.csproj"
COPY "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj" "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.DelugeService.BackgroundJobs/MediaInAction.DelugeService.BackgroundJobs.csproj" "src/services/MediaInAction.DelugeService.BackgroundJobs/MediaInAction.DelugeService.BackgroundJobs.csproj"
COPY "src/services/MediaInAction.DelugeService.UnitTests/MediaInAction.DelugeService.UnitTests.csproj" "src/services/MediaInAction.DelugeService.BackgroundJobs.UnitTests/MediaInAction.DelugeService.BackgroundJobs.UnitTests.csproj"
COPY "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj" "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.DelugeService.Domain.Shared/MediaInAction.DelugeService.Domain.Shared.csproj" "src/services/MediaInAction.DelugeService.Domain.Shared/MediaInAction.DelugeService.Domain.Shared.csproj"
COPY "src/services/MediaInAction.DelugeService.Domain.UnitTests/MediaInAction.DelugeService.Domain.UnitTests.csproj" "src/services/MediaInAction.DelugeService.Domain.Shared.UnitTests/MediaInAction.DelugeService.Domain.Shared.UnitTests.csproj"
COPY "src/services/MediaInAction.DelugeService.Domain.FunctionalTests/MediaInAction.DelugeService.Domain.FunctionalTests.csproj" "src/services/MediaInAction.DelugeService.Domain.FunctionalTests/MediaInAction.DelugeService.Domain.FunctionalTests.csproj"

COPY "src/services/MediaInAction.DelugeService.Application.Contracts/MediaInAction.DelugeService.Application.Contracts.csproj" "src/services/MediaInAction.DelugeService.Application.Contracts/MediaInAction.DelugeService.Application.Contracts.csproj"
COPY "src/services/MediaInAction.DelugeService.Application.UnitTests/MediaInAction.DelugeService.Application.UnitTests.csproj" "src/services/MediaInAction.DelugeService.Application.Contracts.UnitTests/MediaInAction.DelugeService.Application.Contracts.UnitTests.csproj"
COPY "src/services/MediaInAction.DelugeService.Application.FunctionalTests/MediaInAction.DelugeService.Application.FunctionalTests.csproj" "src/services/MediaInAction.DelugeService.Application.FunctionalTests/MediaInAction.DelugeService.Application.FunctionalTests.csproj"

COPY "src/services/MediaInAction.DelugeService.Domain/MediaInAction.DelugeService.Domain.csproj" "src/services/MediaInAction.DelugeService.Domain/MediaInAction.DelugeService.Domain.csproj"
COPY "src/services/MediaInAction.DelugeService.UnitTests/MediaInAction.DelugeService.UnitTests.csproj" "src/services/MediaInAction.DelugeService.Domain.UnitTests/MediaInAction.DelugeService.Domain.UnitTests.csproj"
COPY "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj" "src/services/MediaInAction.DelugeService.FunctionalTests/MediaInAction.DelugeService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.EmbyService.MongoDb.Tests/MediaInAction.EmbyService.MongoDb.Tests.csproj" "src/services/MediaInAction.EmbyService.MongoDb.Tests/MediaInAction.EmbyService.MongoDb.Tests.csproj"
COPY "src/services/MediaInAction.EmbyService.MongoDb.UnitTests/MediaInAction.EmbyService.MongoDb.UnitTests.csproj" "src/services/MediaInAction.EmbyService.MongoDb.Tests.UnitTests/MediaInAction.EmbyService.MongoDb.Tests.UnitTests.csproj"
COPY "src/services/MediaInAction.EmbyService.MongoDb.FunctionalTests/MediaInAction.EmbyService.MongoDb.FunctionalTests.csproj" "src/services/MediaInAction.EmbyService.MongoDb.FunctionalTests/MediaInAction.EmbyService.MongoDb.FunctionalTests.csproj"

COPY "src/services/MediaInAction.EmbyService.Domain/MediaInAction.EmbyService.Domain.csproj" "src/services/MediaInAction.EmbyService.Domain/MediaInAction.EmbyService.Domain.csproj"
COPY "src/services/MediaInAction.EmbyService.UnitTests/MediaInAction.EmbyService.UnitTests.csproj" "src/services/MediaInAction.EmbyService.Domain.UnitTests/MediaInAction.EmbyService.Domain.UnitTests.csproj"
COPY "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj" "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.EmbyService.Lib/MediaInAction.EmbyService.Lib.csproj" "src/services/MediaInAction.EmbyService.Lib/MediaInAction.EmbyService.Lib.csproj"
COPY "src/services/MediaInAction.EmbyService.UnitTests/MediaInAction.EmbyService.UnitTests.csproj" "src/services/MediaInAction.EmbyService.Lib.UnitTests/MediaInAction.EmbyService.Lib.UnitTests.csproj"
COPY "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj" "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.EmbyService.HttpApi.Client/MediaInAction.EmbyService.HttpApi.Client.csproj" "src/services/MediaInAction.EmbyService.HttpApi.Client/MediaInAction.EmbyService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.EmbyService.HttpApi.UnitTests/MediaInAction.EmbyService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.EmbyService.HttpApi.Client.UnitTests/MediaInAction.EmbyService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.EmbyService.HttpApi.FunctionalTests/MediaInAction.EmbyService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.EmbyService.HttpApi.FunctionalTests/MediaInAction.EmbyService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.EmbyService.MongoDb/MediaInAction.EmbyService.MongoDb.csproj" "src/services/MediaInAction.EmbyService.MongoDb/MediaInAction.EmbyService.MongoDb.csproj"
COPY "src/services/MediaInAction.EmbyService.UnitTests/MediaInAction.EmbyService.UnitTests.csproj" "src/services/MediaInAction.EmbyService.MongoDb.UnitTests/MediaInAction.EmbyService.MongoDb.UnitTests.csproj"
COPY "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj" "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.EmbyService.HttpApi/MediaInAction.EmbyService.HttpApi.csproj" "src/services/MediaInAction.EmbyService.HttpApi/MediaInAction.EmbyService.HttpApi.csproj"
COPY "src/services/MediaInAction.EmbyService.UnitTests/MediaInAction.EmbyService.UnitTests.csproj" "src/services/MediaInAction.EmbyService.HttpApi.UnitTests/MediaInAction.EmbyService.HttpApi.UnitTests.csproj"
COPY "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj" "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.EmbyService.HttpApi.Host/MediaInAction.EmbyService.HttpApi.Host.csproj" "src/services/MediaInAction.EmbyService.HttpApi.Host/MediaInAction.EmbyService.HttpApi.Host.csproj"
COPY "src/services/MediaInAction.EmbyService.HttpApi.UnitTests/MediaInAction.EmbyService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.EmbyService.HttpApi.Host.UnitTests/MediaInAction.EmbyService.HttpApi.Host.UnitTests.csproj"
COPY "src/services/MediaInAction.EmbyService.HttpApi.FunctionalTests/MediaInAction.EmbyService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.EmbyService.HttpApi.FunctionalTests/MediaInAction.EmbyService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.EmbyService.Domain.Shared/MediaInAction.EmbyService.Domain.Shared.csproj" "src/services/MediaInAction.EmbyService.Domain.Shared/MediaInAction.EmbyService.Domain.Shared.csproj"
COPY "src/services/MediaInAction.EmbyService.Domain.UnitTests/MediaInAction.EmbyService.Domain.UnitTests.csproj" "src/services/MediaInAction.EmbyService.Domain.Shared.UnitTests/MediaInAction.EmbyService.Domain.Shared.UnitTests.csproj"
COPY "src/services/MediaInAction.EmbyService.Domain.FunctionalTests/MediaInAction.EmbyService.Domain.FunctionalTests.csproj" "src/services/MediaInAction.EmbyService.Domain.FunctionalTests/MediaInAction.EmbyService.Domain.FunctionalTests.csproj"

COPY "src/services/MediaInAction.EmbyService.BackgroundJobs/MediaInAction.EmbyService.BackgroundJobs.csproj" "src/services/MediaInAction.EmbyService.BackgroundJobs/MediaInAction.EmbyService.BackgroundJobs.csproj"
COPY "src/services/MediaInAction.EmbyService.UnitTests/MediaInAction.EmbyService.UnitTests.csproj" "src/services/MediaInAction.EmbyService.BackgroundJobs.UnitTests/MediaInAction.EmbyService.BackgroundJobs.UnitTests.csproj"
COPY "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj" "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.EmbyService.BackgroundWorkers/MediaInAction.EmbyService.BackgroundWorkers.csproj" "src/services/MediaInAction.EmbyService.BackgroundWorkers/MediaInAction.EmbyService.BackgroundWorkers.csproj"
COPY "src/services/MediaInAction.EmbyService.UnitTests/MediaInAction.EmbyService.UnitTests.csproj" "src/services/MediaInAction.EmbyService.BackgroundWorkers.UnitTests/MediaInAction.EmbyService.BackgroundWorkers.UnitTests.csproj"
COPY "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj" "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.EmbyService.HttpApi.Client.ConsoleTestApp/MediaInAction.EmbyService.HttpApi.Client.ConsoleTestApp.csproj" "src/services/MediaInAction.EmbyService.HttpApi.Client.ConsoleTestApp/MediaInAction.EmbyService.HttpApi.Client.ConsoleTestApp.csproj"
COPY "src/services/MediaInAction.EmbyService.HttpApi.Client.UnitTests/MediaInAction.EmbyService.HttpApi.Client.UnitTests.csproj" "src/services/MediaInAction.EmbyService.HttpApi.Client.ConsoleTestApp.UnitTests/MediaInAction.EmbyService.HttpApi.Client.ConsoleTestApp.UnitTests.csproj"
COPY "src/services/MediaInAction.EmbyService.HttpApi.Client.FunctionalTests/MediaInAction.EmbyService.HttpApi.Client.FunctionalTests.csproj" "src/services/MediaInAction.EmbyService.HttpApi.Client.FunctionalTests/MediaInAction.EmbyService.HttpApi.Client.FunctionalTests.csproj"

COPY "src/services/MediaInAction.EmbyService.Application/MediaInAction.EmbyService.Application.csproj" "src/services/MediaInAction.EmbyService.Application/MediaInAction.EmbyService.Application.csproj"
COPY "src/services/MediaInAction.EmbyService.UnitTests/MediaInAction.EmbyService.UnitTests.csproj" "src/services/MediaInAction.EmbyService.Application.UnitTests/MediaInAction.EmbyService.Application.UnitTests.csproj"
COPY "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj" "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.EmbyService.UnitTests/MediaInAction.EmbyService.UnitTests.csproj" "src/services/MediaInAction.EmbyService.UnitTests/MediaInAction.EmbyService.UnitTests.csproj"
COPY "src/services/MediaInAction.EmbyService.UnitTests/MediaInAction.EmbyService.UnitTests.csproj" "src/services/MediaInAction.EmbyService.UnitTests.UnitTests/MediaInAction.EmbyService.UnitTests.UnitTests.csproj"
COPY "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj" "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.EmbyService.Application.Contracts/MediaInAction.EmbyService.Application.Contracts.csproj" "src/services/MediaInAction.EmbyService.Application.Contracts/MediaInAction.EmbyService.Application.Contracts.csproj"
COPY "src/services/MediaInAction.EmbyService.Application.UnitTests/MediaInAction.EmbyService.Application.UnitTests.csproj" "src/services/MediaInAction.EmbyService.Application.Contracts.UnitTests/MediaInAction.EmbyService.Application.Contracts.UnitTests.csproj"
COPY "src/services/MediaInAction.EmbyService.Application.FunctionalTests/MediaInAction.EmbyService.Application.FunctionalTests.csproj" "src/services/MediaInAction.EmbyService.Application.FunctionalTests/MediaInAction.EmbyService.Application.FunctionalTests.csproj"

COPY "src/services/MediaInAction.EmbyService.TestBase/MediaInAction.EmbyService.TestBase.csproj" "src/services/MediaInAction.EmbyService.TestBase/MediaInAction.EmbyService.TestBase.csproj"
COPY "src/services/MediaInAction.EmbyService.UnitTests/MediaInAction.EmbyService.UnitTests.csproj" "src/services/MediaInAction.EmbyService.TestBase.UnitTests/MediaInAction.EmbyService.TestBase.UnitTests.csproj"
COPY "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj" "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj" "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj"
COPY "src/services/MediaInAction.EmbyService.UnitTests/MediaInAction.EmbyService.UnitTests.csproj" "src/services/MediaInAction.EmbyService.FunctionalTests.UnitTests/MediaInAction.EmbyService.FunctionalTests.UnitTests.csproj"
COPY "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj" "src/services/MediaInAction.EmbyService.FunctionalTests/MediaInAction.EmbyService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.AdministrationService.HttpApi/MediaInAction.AdministrationService.HttpApi.csproj" "src/services/MediaInAction.AdministrationService.HttpApi/MediaInAction.AdministrationService.HttpApi.csproj"
COPY "src/services/MediaInAction.AdministrationService.UnitTests/MediaInAction.AdministrationService.UnitTests.csproj" "src/services/MediaInAction.AdministrationService.HttpApi.UnitTests/MediaInAction.AdministrationService.HttpApi.UnitTests.csproj"
COPY "src/services/MediaInAction.AdministrationService.FunctionalTests/MediaInAction.AdministrationService.FunctionalTests.csproj" "src/services/MediaInAction.AdministrationService.FunctionalTests/MediaInAction.AdministrationService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.AdministrationService.EntityFrameworkCore.Tests/MediaInAction.AdministrationService.EntityFrameworkCore.Tests.csproj" "src/services/MediaInAction.AdministrationService.EntityFrameworkCore.Tests/MediaInAction.AdministrationService.EntityFrameworkCore.Tests.csproj"
COPY "src/services/MediaInAction.AdministrationService.EntityFrameworkCore.UnitTests/MediaInAction.AdministrationService.EntityFrameworkCore.UnitTests.csproj" "src/services/MediaInAction.AdministrationService.EntityFrameworkCore.Tests.UnitTests/MediaInAction.AdministrationService.EntityFrameworkCore.Tests.UnitTests.csproj"
COPY "src/services/MediaInAction.AdministrationService.EntityFrameworkCore.FunctionalTests/MediaInAction.AdministrationService.EntityFrameworkCore.FunctionalTests.csproj" "src/services/MediaInAction.AdministrationService.EntityFrameworkCore.FunctionalTests/MediaInAction.AdministrationService.EntityFrameworkCore.FunctionalTests.csproj"

COPY "src/services/MediaInAction.AdministrationService.HttpApi.Client/MediaInAction.AdministrationService.HttpApi.Client.csproj" "src/services/MediaInAction.AdministrationService.HttpApi.Client/MediaInAction.AdministrationService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.AdministrationService.HttpApi.UnitTests/MediaInAction.AdministrationService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.AdministrationService.HttpApi.Client.UnitTests/MediaInAction.AdministrationService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.AdministrationService.HttpApi.FunctionalTests/MediaInAction.AdministrationService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.AdministrationService.HttpApi.FunctionalTests/MediaInAction.AdministrationService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.AdministrationService.FunctionalTests/MediaInAction.AdministrationService.FunctionalTests.csproj" "src/services/MediaInAction.AdministrationService.FunctionalTests/MediaInAction.AdministrationService.FunctionalTests.csproj"
COPY "src/services/MediaInAction.AdministrationService.UnitTests/MediaInAction.AdministrationService.UnitTests.csproj" "src/services/MediaInAction.AdministrationService.FunctionalTests.UnitTests/MediaInAction.AdministrationService.FunctionalTests.UnitTests.csproj"
COPY "src/services/MediaInAction.AdministrationService.FunctionalTests/MediaInAction.AdministrationService.FunctionalTests.csproj" "src/services/MediaInAction.AdministrationService.FunctionalTests/MediaInAction.AdministrationService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.AdministrationService.UnitTests/MediaInAction.AdministrationService.UnitTests.csproj" "src/services/MediaInAction.AdministrationService.UnitTests/MediaInAction.AdministrationService.UnitTests.csproj"
COPY "src/services/MediaInAction.AdministrationService.UnitTests/MediaInAction.AdministrationService.UnitTests.csproj" "src/services/MediaInAction.AdministrationService.UnitTests.UnitTests/MediaInAction.AdministrationService.UnitTests.UnitTests.csproj"
COPY "src/services/MediaInAction.AdministrationService.FunctionalTests/MediaInAction.AdministrationService.FunctionalTests.csproj" "src/services/MediaInAction.AdministrationService.FunctionalTests/MediaInAction.AdministrationService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.AdministrationService.Domain.Shared/MediaInAction.AdministrationService.Domain.Shared.csproj" "src/services/MediaInAction.AdministrationService.Domain.Shared/MediaInAction.AdministrationService.Domain.Shared.csproj"
COPY "src/services/MediaInAction.AdministrationService.Domain.UnitTests/MediaInAction.AdministrationService.Domain.UnitTests.csproj" "src/services/MediaInAction.AdministrationService.Domain.Shared.UnitTests/MediaInAction.AdministrationService.Domain.Shared.UnitTests.csproj"
COPY "src/services/MediaInAction.AdministrationService.Domain.FunctionalTests/MediaInAction.AdministrationService.Domain.FunctionalTests.csproj" "src/services/MediaInAction.AdministrationService.Domain.FunctionalTests/MediaInAction.AdministrationService.Domain.FunctionalTests.csproj"

COPY "src/services/MediaInAction.AdministrationService.EntityFrameworkCore/MediaInAction.AdministrationService.EntityFrameworkCore.csproj" "src/services/MediaInAction.AdministrationService.EntityFrameworkCore/MediaInAction.AdministrationService.EntityFrameworkCore.csproj"
COPY "src/services/MediaInAction.AdministrationService.UnitTests/MediaInAction.AdministrationService.UnitTests.csproj" "src/services/MediaInAction.AdministrationService.EntityFrameworkCore.UnitTests/MediaInAction.AdministrationService.EntityFrameworkCore.UnitTests.csproj"
COPY "src/services/MediaInAction.AdministrationService.FunctionalTests/MediaInAction.AdministrationService.FunctionalTests.csproj" "src/services/MediaInAction.AdministrationService.FunctionalTests/MediaInAction.AdministrationService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.AdministrationService.Application/MediaInAction.AdministrationService.Application.csproj" "src/services/MediaInAction.AdministrationService.Application/MediaInAction.AdministrationService.Application.csproj"
COPY "src/services/MediaInAction.AdministrationService.UnitTests/MediaInAction.AdministrationService.UnitTests.csproj" "src/services/MediaInAction.AdministrationService.Application.UnitTests/MediaInAction.AdministrationService.Application.UnitTests.csproj"
COPY "src/services/MediaInAction.AdministrationService.FunctionalTests/MediaInAction.AdministrationService.FunctionalTests.csproj" "src/services/MediaInAction.AdministrationService.FunctionalTests/MediaInAction.AdministrationService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.AdministrationService.Application.Contracts/MediaInAction.AdministrationService.Application.Contracts.csproj" "src/services/MediaInAction.AdministrationService.Application.Contracts/MediaInAction.AdministrationService.Application.Contracts.csproj"
COPY "src/services/MediaInAction.AdministrationService.Application.UnitTests/MediaInAction.AdministrationService.Application.UnitTests.csproj" "src/services/MediaInAction.AdministrationService.Application.Contracts.UnitTests/MediaInAction.AdministrationService.Application.Contracts.UnitTests.csproj"
COPY "src/services/MediaInAction.AdministrationService.Application.FunctionalTests/MediaInAction.AdministrationService.Application.FunctionalTests.csproj" "src/services/MediaInAction.AdministrationService.Application.FunctionalTests/MediaInAction.AdministrationService.Application.FunctionalTests.csproj"

COPY "src/services/MediaInAction.AdministrationService.Domain/MediaInAction.AdministrationService.Domain.csproj" "src/services/MediaInAction.AdministrationService.Domain/MediaInAction.AdministrationService.Domain.csproj"
COPY "src/services/MediaInAction.AdministrationService.UnitTests/MediaInAction.AdministrationService.UnitTests.csproj" "src/services/MediaInAction.AdministrationService.Domain.UnitTests/MediaInAction.AdministrationService.Domain.UnitTests.csproj"
COPY "src/services/MediaInAction.AdministrationService.FunctionalTests/MediaInAction.AdministrationService.FunctionalTests.csproj" "src/services/MediaInAction.AdministrationService.FunctionalTests/MediaInAction.AdministrationService.FunctionalTests.csproj"

COPY "src/services/MediaInAction.AdministrationService.HttpApi.Host/MediaInAction.AdministrationService.HttpApi.Host.csproj" "src/services/MediaInAction.AdministrationService.HttpApi.Host/MediaInAction.AdministrationService.HttpApi.Host.csproj"
COPY "src/services/MediaInAction.AdministrationService.HttpApi.UnitTests/MediaInAction.AdministrationService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.AdministrationService.HttpApi.Host.UnitTests/MediaInAction.AdministrationService.HttpApi.Host.UnitTests.csproj"
COPY "src/services/MediaInAction.AdministrationService.HttpApi.FunctionalTests/MediaInAction.AdministrationService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.AdministrationService.HttpApi.FunctionalTests/MediaInAction.AdministrationService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.AdministrationService.TestBase/MediaInAction.AdministrationService.TestBase.csproj" "src/services/MediaInAction.AdministrationService.TestBase/MediaInAction.AdministrationService.TestBase.csproj"
COPY "src/services/MediaInAction.AdministrationService.UnitTests/MediaInAction.AdministrationService.UnitTests.csproj" "src/services/MediaInAction.AdministrationService.TestBase.UnitTests/MediaInAction.AdministrationService.TestBase.UnitTests.csproj"
COPY "src/services/MediaInAction.AdministrationService.FunctionalTests/MediaInAction.AdministrationService.FunctionalTests.csproj" "src/services/MediaInAction.AdministrationService.FunctionalTests/MediaInAction.AdministrationService.FunctionalTests.csproj"

# tests
COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

# src
COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

# src
COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

# src
COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

# src
COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

COPY "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "src/services/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.UnitTests/MediaInAction.FileService.HttpApi.UnitTests.csproj" "src/services/MediaInAction.FileService.HttpApi.Client.UnitTests/MediaInAction.FileService.HttpApi.Client.UnitTests.csproj"
COPY "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj" "src/services/MediaInAction.FileService.HttpApi.FunctionalTests/MediaInAction.FileService.HttpApi.FunctionalTests.csproj"

# End build cache

RUN dotnet restore "MediaInAction.sln"
COPY . .
WORKDIR /work/src/src/services/MediaInAction.FileService.HttpApi.Client
RUN dotnet publish --no-restore -c Release -o /app

FROM build as unit_tests
WORKDIR /work/src/src/services/MediaInAction.FileService.HttpApi.Client.UnitTests

FROM build as functional_tests
WORKDIR /work/src/src/services/MediaInAction.FileService.HttpApi.Client.FunctionalTests

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MediaInAction.FileService.HttpApi.Client.dll"]
