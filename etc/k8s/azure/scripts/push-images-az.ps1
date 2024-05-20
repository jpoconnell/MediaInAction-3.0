param ($version='latest')

az acr login --name volosoft

docker tag mia/app-web:$version volosoft.azurecr.io/mia/app-web:$version
docker push volosoft.azurecr.io/mia/app-web:$version
docker tag volosoft.azurecr.io/mia/app-web:$version volosoft.azurecr.io/mia/dbmigrator:latest

docker tag mia/app-authserver:$version volosoft.azurecr.io/mia/app-authserver:$version
docker push volosoft.azurecr.io/mia/app-authserver:$version
docker tag volosoft.azurecr.io/mia/app-authserver:$version volosoft.azurecr.io/mia/app-authserver:latest

docker tag mia/app-publicweb:$version volosoft.azurecr.io/mia/app-publicweb:$version
docker push volosoft.azurecr.io/mia/app-publicweb:$version
docker tag volosoft.azurecr.io/mia/app-publicweb:$version volosoft.azurecr.io/mia/app-publicweb:latest

docker tag mia/gateway-web:$version volosoft.azurecr.io/mia/gateway-web:$version
docker push volosoft.azurecr.io/mia/gateway-web:$version
docker tag volosoft.azurecr.io/mia/gateway-web:$version volosoft.azurecr.io/mia/gateway-web:latest

docker tag mia/gateway-web-public:$version volosoft.azurecr.io/mia/gateway-web-public:$version
docker push volosoft.azurecr.io/mia/gateway-web-public:$version
docker tag volosoft.azurecr.io/mia/gateway-web-public:$version volosoft.azurecr.io/mia/gateway-web-public:latest

docker tag mia/service-identity:$version volosoft.azurecr.io/mia/service-identity:$version
docker push volosoft.azurecr.io/mia/service-identity:$version
docker tag volosoft.azurecr.io/mia/service-identity:$version volosoft.azurecr.io/mia/service-identity:latest

docker tag mia/service-administration:$version volosoft.azurecr.io/mia/service-administration:$version
docker push volosoft.azurecr.io/mia/service-administration:$version
docker tag volosoft.azurecr.io/mia/service-administration:$version volosoft.azurecr.io/mia/service-administration:latest

docker tag mia/service-catalog:$version volosoft.azurecr.io/mia/service-catalog:$version
docker push volosoft.azurecr.io/mia/service-catalog:$version
docker tag volosoft.azurecr.io/mia/service-catalog:$version volosoft.azurecr.io/mia/service-catalog:latest

docker tag mia/service-basket:$version volosoft.azurecr.io/mia/service-basket:$version
docker push volosoft.azurecr.io/mia/service-basket:$version
docker tag volosoft.azurecr.io/mia/service-basket:$version volosoft.azurecr.io/mia/service-basket:latest

docker tag mia/service-payment:$version volosoft.azurecr.io/mia/service-payment:$version
docker push volosoft.azurecr.io/mia/service-payment:$version
docker tag volosoft.azurecr.io/mia/service-payment:$version volosoft.azurecr.io/mia/service-payment:latest

docker tag mia/service-ordering:$version volosoft.azurecr.io/mia/service-ordering:$version
docker push volosoft.azurecr.io/mia/service-ordering:$version
docker tag volosoft.azurecr.io/mia/service-ordering:$version volosoft.azurecr.io/mia/service-ordering:latest

docker tag mia/service-cmskit:$version volosoft.azurecr.io/mia/service-cmskit:$version
docker push volosoft.azurecr.io/mia/service-cmskit:$version
docker tag volosoft.azurecr.io/mia/service-cmskit:$version volosoft.azurecr.io/mia/service-cmskit:latest



