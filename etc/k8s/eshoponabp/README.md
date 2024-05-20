# abp-charts

# MediaInAction
This project is a reference for who want to build microservice solutions with the ABP Framework.

## Pre-requirement

* [Helm](https://helm.sh) must be installed to use the charts.
Please refer to Helm's [documentation](https://helm.sh/docs/) to get started.
* Install [NGINX ingress](https://kubernetes.github.io/ingress-nginx/deploy/) for k8s or Install NGINX ingress using helm
  ```powershell
  helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx
  helm repo update

  helm upgrade --install --version=4.0.19 ingress-nginx ingress-nginx/ingress-nginx
  ```

Once Helm is set up properly, add the repo as follows:

```console
helm repo add abp-charts https://abpframework.github.io/abp-charts/mia
```

Initial authentication data (redirectURIs etc) are seeded based on **mia-st** name and **mia** namespace for the deployment.

## Configuring HTTPS

You can also run the staging solution on your local Kubernetes Cluster with HTTPS. There are various ways to create self-signed certificate. 

### Installing mkcert
This guide will use mkcert to create self-signed certificates.

Follow the [installation guide](https://github.com/FiloSottile/mkcert#installation) to install mkcert.

### Creating mkcert Root CA
Use the command to create root (local) certificate authority for your certificates:
```powershell
mkcert -install
```

**Note:** all the certificates created by mkcert certificate creation will be trusted by local machine

### Run mkcert

Create certificate for the miaOnAbp domains using the mkcert command below:
```powershell
mkcert "mia.dev" "*.mia.dev"
```

At the end of the output you will see something like

The certificate is at "./mia.dev+1.pem" and the key at "./mia.dev+1-key.pem"

Copy the cert name and key name below to create tls secret

```powershell
kubectl create namespace mia
kubectl create secret tls -n mia mia-wildcard-tls --cert=./mia.dev+1.pem --key=./mia.dev+1-key.pem
```

## How to run?

* Add entries to the hosts file (in Windows: `C:\Windows\System32\drivers\etc\hosts`, in linux and macos: `/etc/hosts` ):

  ````powershell
127.0.0.1 admin.mia.dev
127.0.0.1 mia.dev
127.0.0.1 account.mia.dev
127.0.0.1 identity.mia.dev
127.0.0.1 administration.mia.dev
127.0.0.1 product.mia.dev
127.0.0.1 basket.mia.dev
127.0.0.1 catalog.mia.dev
127.0.0.1 ordering.mia.dev
127.0.0.1 cmskit.mia.dev
127.0.0.1 payment.mia.dev
127.0.0.1 gateway-web.mia.dev
127.0.0.1 gateway-public.mia.dev
  ````

* Run `helm upgrade --install mia-st abp-charts/mia --namespace mia --create-namespace`
* *You may wait ~30 seconds on first run for preparing the database*.
* Browse https://mia-st-public-web for public and https://mia-st-web for web application
* Username: `admin`, password: `1q2w3E*`.
