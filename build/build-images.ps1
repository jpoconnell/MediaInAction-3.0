param ($version='latest')

$currentFolder = $PSScriptRoot
$slnFolder = Join-Path $currentFolder "../"
# Apps
$webAppFolder = Join-Path $slnFolder "apps/angular"
$publicWebFolder = Join-Path $slnFolder "apps/public-web/src/EShopOnAbp.PublicWeb"
# Gateways
$webGatewayFolder = Join-Path $slnFolder "gateways/web/src/EShopOnAbp.WebGateway"
$webPublicGatewayFolder = Join-Path $slnFolder "gateways/web-public/src/EShopOnAbp.WebPublicGateway"
# Microservices
$identityServiceFolder = Join-Path $slnFolder "services/identity/src/EShopOnAbp.IdentityService.HttpApi.Host"
$administrationServiceFolder = Join-Path $slnFolder "services/administration/src/EShopOnAbp.AdministrationService.HttpApi.Host"
$basketServiceFolder = Join-Path $slnFolder "services/basket/src/EShopOnAbp.BasketService"
$catalogServiceFolder = Join-Path $slnFolder "services/catalog/src/EShopOnAbp.CatalogService.HttpApi.Host"
$paymentServiceFolder = Join-Path $slnFolder "services/payment/src/EShopOnAbp.PaymentService.HttpApi.Host"
$orderingServiceFolder = Join-Path $slnFolder "services/ordering/src/EShopOnAbp.OrderingService.HttpApi.Host"
$cmskitServiceFolder = Join-Path $slnFolder "services/cmskit/src/EShopOnAbp.CmskitService.HttpApi.Host"

$total = 11

Write-Host "===== BUILDING APPLICATIONS =====" -ForegroundColor Yellow

### Angular WEB App
Write-Host "*** BUILDING ANGULAR WEB APPLICATION 1/$total ***" -ForegroundColor Green
Set-Location $webAppFolder
docker build -f "$webAppFolder/Dockerfile" -t mia/app-web:$version .

### PUBLIC-WEB
Write-Host "**************** BUILDING WEB-PUBLIC 2/$total ****************" -ForegroundColor Green
Set-Location $slnFolder
docker build -f "$publicWebFolder/Dockerfile" -t mia/app-publicweb:$version .

Write-Host "===== BUILDING GATEWAYS =====" -ForegroundColor Yellow 

### WEB-GATEWAY
Write-Host "**************** BUILDING WEB-GATEWAY 3/$total ****************" -ForegroundColor Green
Set-Location $slnFolder
docker build -f "$webGatewayFolder/Dockerfile" -t mia/gateway-web:$version .

### PUBLICWEB-GATEWAY
Write-Host "**************** BUILDING WEB-PUBLIC-GATEWAY 4/$total ****************" -ForegroundColor Green
Set-Location $slnFolder
docker build -f "$webPublicGatewayFolder/Dockerfile" -t mia/gateway-web-public:$version .

Write-Host "===== BUILDING MICROSERVICES =====" -ForegroundColor Yellow

### IDENTITY-SERVICE
Write-Host "**************** BUILDING IDENTITY-SERVICE 5/$total ****************" -ForegroundColor Green
Set-Location $slnFolder
docker build -f "$identityServiceFolder/Dockerfile" -t mia/service-identity:$version .

### ADMINISTRATION-SERVICE
Write-Host "**************** BUILDING ADMINISTRATION-SERVICE 6/$total ****************" -ForegroundColor Green
Set-Location $slnFolder
docker build -f "$administrationServiceFolder/Dockerfile" -t mia/service-administration:$version .

### BASKET-SERVICE
Write-Host "**************** BUILDING BASKET-SERVICE 7/$total ****************" -ForegroundColor Green
Set-Location $slnFolder
docker build -f "$basketServiceFolder/Dockerfile" -t mia/service-basket:$version .

### CATALOG-SERVICE
Write-Host "**************** BUILDING CATALOG-SERVICE 8/$total ****************" -ForegroundColor Green
Set-Location $slnFolder
docker build -f "$catalogServiceFolder/Dockerfile" -t mia/service-catalog:$version .

### PAYMENT-SERVICE
Write-Host "**************** BUILDING PAYMENT-SERVICE 9/$total ****************" -ForegroundColor Green
Set-Location $slnFolder
docker build -f "$paymentServiceFolder/Dockerfile" -t mia/service-payment:$version .

### ORDERING-SERVICE
Write-Host "**************** BUILDING ORDERING-SERVICE 10/$total ****************" -ForegroundColor Green
Set-Location $slnFolder
docker build -f "$orderingServiceFolder/Dockerfile" -t mia/service-ordering:$version .

### CMSKIT-SERVICE
Write-Host "**************** BUILDING CMSKIT-SERVICE 11/$total ****************" -ForegroundColor Green
Set-Location $slnFolder
docker build -f "$cmskitServiceFolder/Dockerfile" -t mia/service-cmskit:$version .

### ALL COMPLETED
Write-Host "ALL COMPLETED" -ForegroundColor Green
Set-Location $currentFolder