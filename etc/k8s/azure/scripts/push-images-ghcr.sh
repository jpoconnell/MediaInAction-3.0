export version="latest"

az acr login --name volocr

docker tag mia/app-web:"${version}" ghcr.io/volosoft/mia/app-web:"${version}"
docker push ghcr.io/volosoft/mia/app-web:"${version}"

docker tag mia/app-authserver:"${version}" ghcr.io/volosoft/mia/app-authserver:"${version}"
docker push ghcr.io/volosoft/mia/app-authserver:"${version}"

docker tag mia/app-publicweb:"${version}" ghcr.io/volosoft/mia/app-publicweb:"${version}"
docker push ghcr.io/volosoft/mia/app-publicweb:"${version}"

docker tag mia/gateway-web:"${version}" ghcr.io/volosoft/mia/gateway-web:"${version}"
docker push ghcr.io/volosoft/mia/gateway-web:"${version}"

docker tag mia/gateway-web-public:"${version}" ghcr.io/volosoft/mia/gateway-web-public:"${version}"
docker push ghcr.io/volosoft/mia/gateway-web-public:"${version}"

docker tag mia/service-identity:"${version}" ghcr.io/volosoft/mia/service-identity:"${version}"
docker push ghcr.io/volosoft/mia/service-identity:"${version}"

docker tag mia/service-administration:"${version}" ghcr.io/volosoft/mia/service-administration:"${version}"
docker push ghcr.io/volosoft/mia/service-administration:"${version}"

docker tag mia/service-basket:"${version}" ghcr.io/volosoft/mia/service-basket:"${version}"
docker push ghcr.io/volosoft/mia/service-basket:"${version}"

docker tag mia/service-catalog:"${version}" ghcr.io/volosoft/mia/service-catalog:"${version}"
docker push ghcr.io/volosoft/mia/service-catalog:"${version}"

docker tag mia/service-ordering:"${version}" ghcr.io/volosoft/mia/service-ordering:"${version}"
docker push ghcr.io/volosoft/mia/service-ordering:"${version}"

docker tag mia/service-cmskit:"${version}" ghcr.io/volosoft/mia/service-cmskit:"${version}"
docker push ghcr.io/volosoft/mia/service-cmskit:"${version}"

docker tag mia/service-payment:"${version}" ghcr.io/volosoft/mia/service-payment:"${version}"
docker push ghcr.io/volosoft/mia/service-payment:"${version}"