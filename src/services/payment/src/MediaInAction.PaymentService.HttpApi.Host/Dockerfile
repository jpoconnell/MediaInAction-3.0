FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# It's important to keep lines from here down to "COPY . ." identical in all Dockerfiles
# to take advantage of Docker's build cache, to speed up local container builds
COPY "MediaInAction.sln" "MediaInAction.sln"

# Applications
COPY "apps/public-web/src/MediaInAction.PublicWeb/MediaInAction.PublicWeb.csproj" "apps/public-web/src/MediaInAction.PublicWeb/MediaInAction.PublicWeb.csproj"

# Shared
COPY "shared/MediaInAction.Shared.Hosting/MediaInAction.Shared.Hosting.csproj" "shared/MediaInAction.Shared.Hosting/MediaInAction.Shared.Hosting.csproj"
COPY "shared/MediaInAction.Shared.Hosting.AspNetCore/MediaInAction.Shared.Hosting.AspNetCore.csproj" "shared/MediaInAction.Shared.Hosting.AspNetCore/MediaInAction.Shared.Hosting.AspNetCore.csproj"
COPY "shared/MediaInAction.Shared.Hosting.Gateways/MediaInAction.Shared.Hosting.Gateways.csproj" "shared/MediaInAction.Shared.Hosting.Gateways/MediaInAction.Shared.Hosting.Gateways.csproj"
COPY "shared/MediaInAction.Shared.Hosting.Microservices/MediaInAction.Shared.Hosting.Microservices.csproj" "shared/MediaInAction.Shared.Hosting.Microservices/MediaInAction.Shared.Hosting.Microservices.csproj"
COPY "shared/MediaInAction.Shared.Localization/MediaInAction.Shared.Localization.csproj" "shared/MediaInAction.Shared.Localization/MediaInAction.Shared.Localization.csproj"

# Microservices
COPY "services/administration/src/MediaInAction.AdministrationService.HttpApi.Host/MediaInAction.AdministrationService.HttpApi.Host.csproj" "services/administration/src/MediaInAction.AdministrationService.HttpApi.Host/MediaInAction.AdministrationService.HttpApi.Host.csproj"
COPY "services/identity/src/MediaInAction.IdentityService.HttpApi.Host/MediaInAction.IdentityService.HttpApi.Host.csproj" "services/identity/src/MediaInAction.IdentityService.HttpApi.Host/MediaInAction.IdentityService.HttpApi.Host.csproj"
COPY "services/basket/src/MediaInAction.BasketService/MediaInAction.BasketService.csproj" "services/basket/src/MediaInAction.BasketService/MediaInAction.BasketService.csproj"
COPY "services/ordering/src/MediaInAction.OrderingService.HttpApi.Host/MediaInAction.OrderingService.HttpApi.Host.csproj" "services/ordering/src/MediaInAction.OrderingService.HttpApi.Host/MediaInAction.OrderingService.HttpApi.Host.csproj"
COPY "services/catalog/src/MediaInAction.CatalogService.HttpApi.Host/MediaInAction.CatalogService.HttpApi.Host.csproj" "services/catalog/src/MediaInAction.CatalogService.HttpApi.Host/MediaInAction.CatalogService.HttpApi.Host.csproj"
COPY "services/payment/src/MediaInAction.PaymentService.HttpApi.Host/MediaInAction.PaymentService.HttpApi.Host.csproj" "services/payment/src/MediaInAction.PaymentService.HttpApi.Host/MediaInAction.PaymentService.HttpApi.Host.csproj"
COPY "services/cmskit/src/MediaInAction.CmskitService.HttpApi.Host/MediaInAction.CmskitService.HttpApi.Host.csproj" "services/cmskit/src/MediaInAction.CmskitService.HttpApi.Host/MediaInAction.CmskitService.HttpApi.Host.csproj"

# Gateways
COPY "gateways/web/src/MediaInAction.WebGateway/MediaInAction.WebGateway.csproj" "gateways/web/src/MediaInAction.WebGateway/MediaInAction.WebGateway.csproj"
COPY "gateways/web-public/src/MediaInAction.WebPublicGateway/MediaInAction.WebPublicGateway.csproj" "gateways/web-public/src/MediaInAction.WebPublicGateway/MediaInAction.WebPublicGateway.csproj"

COPY "NuGet.Config" "NuGet.Config"

RUN dotnet restore "MediaInAction.sln"
COPY . .

WORKDIR "/src/services/payment/src/MediaInAction.PaymentService.HttpApi.Host"
RUN dotnet publish -c Release -o /app 
# RUN dotnet publish --no-restore -c Release -o /app 

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MediaInAction.PaymentService.HttpApi.Host.dll"]