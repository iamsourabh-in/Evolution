
#


## Install dependencies

```sh

#get kubectl
snap install kubectl --classic

# Update the apt package index, and install the latest version of Docker Engine, containerd, and Docker Compose, or go to the next step to install a specific version

#remove older versions
sudo apt-get remove docker docker-engine docker.io containerd runc

 sudo apt-get update
 sudo apt-get install docker-ce docker-ce-cli containerd.io docker-compose-plugin

```

## Create your own K8 cluster
### We would be using Kind for the same


```sh

curl -Lo ./kind https://kind.sigs.k8s.io/dl/v0.15.0/kind-linux-amd64
chmod +x ./kind
sudo mv ./kind /usr/local/bin/kind


kind create cluster --name kind

kubectl cluster-info

kubectl get pods -n 
```


## Create you service Mesh to work with 

### We would be using Kuma Mesh for the same

```sh

curl -L https://kuma.io/installer.sh | VERSION=1.8.0 sh -

#I suggest adding the kumactl executable to your PATH (by executing: PATH=$(pwd):$PATH) so that it's always available in every working directory. 
ln -s $PWD/kumactl /usr/local/bin/kumactl


#Finally, we can install and run Kuma:
kumactl install control-plane | kubectl apply -f -


#Kuma ships with a read-only GUI that you can use to retrieve Kuma resources. By default the GUI listens on the API port and defaults to :5681/gui.
#To access Kuma we need to first port-forward the API service with:

kubectl port-forward svc/kuma-control-plane -n kuma-system 5681:5681

# Now you will notice that Kuma automatically creates a Mesh entity with name default

```


## Steps to Deploy to Kubernetes

```sh

#Secret for container to fetch
kubectl create secret generic mssql --from-literal=SA_PASSWORD="password@1"

# Setup Rabbit MQ on Kubernetes
kubectl apply -f .\Deploy\K8s\rabbitmq\

# Start the SQL Server for Services to Use.
kubectl apply -f Deploy/K8S/sql

# Starting the Platform Service
kubectl apply -f Deploy/K8S/platform-service

# Exposing a port to (Just to Check if the service is weorking)
kubectl port-forward <pod-name> 8090:80

# Deploying the command services
kubectl apply -f .\Deploy\K8s\command-service\

#Check if all pods are running.
kubectl get pods -A

#-----------------------------------------------------------------------------------------------
#EXAMPLE OUTPUT
#-----------------------------------------------------------------------------------------------

# NAMESPACE     NAME                                     READY   STATUS    RESTARTS         AGE
  default       command-depl-57f87776d5-x56t6            1/1     Running   0                87s
  default       mssql-depl-64f6bb7c7b-dxg5d              1/1     Running   0                151m
  default       platforms-depl-c86c96778-sd2gf           1/1     Running   0                150m
  default       rabbitmq-depl-5778c64d97-9nzs9           1/1     Running   0                18m

#-----------------------------------------------------------------------------------------------

# Now , we have to define the ingress.
# for this we need : nginx

# Create the ingress controller
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.1.2/deploy/static/provider/cloud/deploy.yaml

# Create the ingress service
kubectl apply -f .\Deploy\K8s\ingress\ingress-nginx-srv.yaml

#create a host entry to point to localhost

cd Deploy/local

.\AddToHost.ps1 -Hostname acme.com -DesiredIP 127.0.0.1
```


## Remove Resources

```sh

kubectl delete -f Deploy/K8S/platform-service


kubectl delete -f Deploy\K8s\command-service\


kubectl delete -f Deploy/K8S/sql


kubectl delete -f Deploy\K8s\ingress\ingress-nginx-srv.yaml


kubectl delete -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.1.2/deploy/static/provider/cloud/deploy.yaml

kubectl delete -f .\Deploy\K8s\rabbitmq\

kubectl delete secret generic mssql 

```
