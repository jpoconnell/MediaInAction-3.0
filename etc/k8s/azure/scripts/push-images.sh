export version="1.0.5"

az acr login --name volocr

docker tag mia/app-web:"${version}" volocr.azurecr.io/mia/app-web:"${version}"
docker push volocr.azurecr.io/mia/app-web:"${version}"

docker tag mia/app-authserver:"${version}" volocr.azurecr.io/mia/app-authserver:"${version}"
docker push volocr.azurecr.io/mia/app-authserver:"${version}"

docker tag mia/app-publicweb:"${version}" volocr.azurecr.io/mia/app-publicweb:"${version}"
docker push volocr.azurecr.io/mia/app-publicweb:"${version}"

docker tag mia/gateway-web:"${version}" volocr.azurecr.io/mia/gateway-web:"${version}"
docker push volocr.azurecr.io/mia/gateway-web:"${version}"

docker tag mia/gateway-web-public:"${version}" volocr.azurecr.io/mia/gateway-web-public:"${version}"
docker push volocr.azurecr.io/mia/gateway-web-public:"${version}"

docker tag mia/service-identity:"${version}" volocr.azurecr.io/mia/service-identity:"${version}"
docker push volocr.azurecr.io/mia/service-identity:"${version}"

docker tag mia/service-administration:"${version}" volocr.azurecr.io/mia/service-administration:"${version}"
docker push volocr.azurecr.io/mia/service-administration:"${version}"

docker tag mia/service-basket:"${version}" volocr.azurecr.io/mia/service-basket:"${version}"
docker push volocr.azurecr.io/mia/service-basket:"${version}"

docker tag mia/service-catalog:"${version}" volocr.azurecr.io/mia/service-catalog:"${version}"
docker push volocr.azurecr.io/mia/service-catalog:"${version}"

docker tag mia/service-ordering:"${version}" volocr.azurecr.io/mia/service-ordering:"${version}"
docker push volocr.azurecr.io/mia/service-ordering:"${version}"

docker tag mia/service-cmskit:"${version}" volocr.azurecr.io/mia/service-cmskit:"${version}"
docker push volocr.azurecr.io/mia/service-cmskit:"${version}"

docker tag mia/service-payment:"${version}" volocr.azurecr.io/mia/service-payment:"${version}"
docker push volocr.azurecr.io/mia/service-payment:"${version}"