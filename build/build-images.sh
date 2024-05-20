#!/bin/bash

export IMAGE_TAG="latest"
export total=11
cd ../
export currentFolder=`pwd`
cd build/

echo "*** BUILDING WEB (WWW) 1/${total} ****************"
cd ${currentFolder}/apps/angular
docker build --force-rm -t "mia/app-web:${IMAGE_TAG}" .


echo "*** BUILDING WEB-PUBLIC 2/$total ****************"
cd ${currentFolder}/apps/public-web/src/EShopOnAbp.PublicWeb
docker build --force-rm -t "mia/app-publicweb:${IMAGE_TAG}" .


echo "*** BUILDING WEB-GATEWAY 3/$total ****************"
cd ${currentFolder}/gateways/web/src/EShopOnAbp.WebGateway
docker build --force-rm -t "mia/gateway-web:${IMAGE_TAG}" .


echo "*** BUILDING WEB-PUBLIC-GATEWAY 4/$total ****************"
cd ${currentFolder}/gateways/web-public/src/EShopOnAbp.WebPublicGateway
docker build --force-rm -t "mia/gateway-web-public:${IMAGE_TAG}" .


echo "*** BUILDING IDENTITY-SERVICE 5/$total ****************"
cd ${currentFolder}/services/identity/src/EShopOnAbp.IdentityService.HttpApi.Host
docker build --force-rm -t "mia/service-identity:${IMAGE_TAG}" .


echo "*** BUILDING ADMINISTRATION-SERVICE 6/$total ****************"
cd ${currentFolder}/services/administration/src/EShopOnAbp.AdministrationService.HttpApi.Host
docker build --force-rm -t "mia/service-administration:${IMAGE_TAG}" .


echo "**************** BUILDING BASKET-SERVICE 7/$total ****************"
cd ${currentFolder}/services/basket/src/EShopOnAbp.BasketService
docker build --force-rm -t "mia/service-basket:${IMAGE_TAG}" .


echo "**************** BUILDING CATALOG-SERVICE 8/$total ****************"
cd ${currentFolder}/services/catalog/src/EShopOnAbp.CatalogService.HttpApi.Host
docker build --force-rm -t "mia/service-catalog:${IMAGE_TAG}" .


echo "**************** BUILDING PAYMENT-SERVICE 9/$total ****************"
cd ${currentFolder}/services/payment/src/EShopOnAbp.PaymentService.HttpApi.Host
docker build --force-rm -t "mia/service-payment:${IMAGE_TAG}" .


echo "**************** BUILDING ORDERING-SERVICE 10/$total ****************"
cd ${currentFolder}/services/ordering/src/EShopOnAbp.OrderingService.HttpApi.Host
docker build --force-rm -t "mia/service-ordering:${IMAGE_TAG}" .

echo "**************** BUILDING CMSKIT-SERVICE 11/$total ****************"
cd ${currentFolder}/services/cmskit/src/EShopOnAbp.CmskitService.HttpApi.Host
docker build --force-rm -t "mia/service-cmskit:${IMAGE_TAG}" .

echo "ALL COMPLETED"