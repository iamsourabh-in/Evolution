
# Evolution 


Learn hot to spin up you own cluster and work with it. [Learn Setup here](https://github.com/iamsourabh-in/Evolution/blob/master/Deploy/setup.md)


## Steps to Deploy to Kubernetes
Setup the `evolution` namespace and we would be working with dev.

You can either setup using `ksutomize` of if you preffer you can indivisually setup the infra

This would
- Create a namespace
- Start a Rabbit MQ Server
- Create a PVC for SQL Server
- Creates a secret for SQL Server
- Starts a SQL Server with the PVC and service attached.

```sh
kubectl apply -k .\Deploy\K8s\kustomize\overlays\dev\
```

or spin up other infra indivisually

```sh
# Start Rabbit MQ on Kubernetes
kubectl apply -f .\Deploy\K8s\rabbitmq\

# Secret for sqlServer
kubectl create secret generic mssql --from-literal=SA_PASSWORD="password@1"

kubectl apply -f .\Deploy\K8s\local-pvc.yaml

# Start the SQL Server for Services to Use.
kubectl apply -f Deploy/K8S/sql
```


## Deploy Services to Kubernetes

```sh
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

#create a host entry to point to localhost for acme.com

cd 

./Deploy/local/AddToHost.ps1 -Hostname acme.com -DesiredIP 127.0.0.1

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
