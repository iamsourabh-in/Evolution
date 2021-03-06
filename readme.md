# Evolution
## _Cloud first approach for building dotnet applications_

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

Evolution is a cloud-enabled, devops-ready practice for any applications.
It focuses primarly on dotnet applications

- Microservices
- Clean Architecture
- Async Communication


![alt text](https://github.com/iamsourabh-in/Evolution/blob/reorganize/docs/frontpage.png)

## Features

- Import a HTML file and watch it magically convert to Markdown
- Drag and drop images (requires your Dropbox account be linked)
- Import and save files from GitHub, Dropbox, Google Drive and One Drive
- Drag and drop markdown and HTML files into Dillinger
- Export documents as Markdown, HTML and PDF


## Tech



- [.NET Core](https://dotnet.microsoft.com/) - Free. Cross-platform. Open source.
A developer platform for building all your apps!
- [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-6.0) - ASP.NET Core to create web apps and services that are fast, secure, cross-platform, and cloud-based
- [Docker](https://www.docker.com/) - OS-level virtualization to deliver software in packages called containers.
- [Kubernetes](https://kubernetes.io/) - Kubernetes is an open-source container orchestration system for automating software deployment, scaling, and management. 
- [Terraform](https://www.terraform.io/) - Terraform is an open-source infrastructure as code software tool created by HashiCorp.




#### Building for source

For production release:

```sh
gulp build --prod
```

Generating pre-built zip archives for distribution:

```sh
gulp build dist --prod
```

# Docker

**Free Software, Hell Yeah!**



```sh

docker build -t rohitrustagi007/platformservice .

docker push rohitrustagi007/platformservice

docker build -t rohitrustagi007/commandservice .

docker push rohitrustagi007/commandservice

```
# Kubenetes


**Deployments**

```sh
kubectl apply -f Deploy/K8S/platform-depl.yaml

kubectl delete deploy platforms-depl 

kubectl delete service platform-cluster-ip

kubectl apply -f Deploy/K8S/command-depl.yaml

kubectl delete deploy command-depl 

kubectl delete service command-cluster-ip

```

**Ingress**

```sh

kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.1.2/deploy/static/provider/cloud/deploy.yaml

kubectl apply -f .\Deploy\K8s\ingress-nginx-srv.yaml

kubectl delete ingress ingress-nginx-srv 

```

**Persistance volume claims**
```sh

kubectl apply -f .\Deploy\K8s\local-pvc.yaml

kubectl delete pvc sqlserver-pvc

```

**Secrets**
```sh
kubectl create secret generic mssql --from-literal=SA_PASSWORD="password@1"
```


**MSSQL Server Express** 
```sh
kubectl apply -f .\Deploy\K8s\mssql-depl.yaml

kubectl delete deploy mssql-depl

kubectl delete service mssql-loadbalancer

kubectl delete service mssql-cluster-ip

```


# Redis
```sh
kubectl apply -f ./Deploy/K8S/redis/redis-config.yaml

kubectl apply -f https://raw.githubusercontent.com/kubernetes/website/main/content/en/examples/pods/config/redis-pod.yaml
OR
kubectl apply -f ./Deploy/K8S/redis/redis-pod.yaml

kubectl get pod/redis configmap/redis-config 

kubectl describe configmap/redis-config

### Further Configurations

kubectl exec -it redis -- redis-cli

127.0.0.1:6379> CONFIG GET maxmemory

127.0.0.1:6379> CONFIG GET maxmemory-policy

```

# Helpers

```sh
kubectl describe pod platforms-depl-85c65658cd-44zc8  

docker info

kubectl get services

kubectl get pvc  -o wide

kubectl get pods

kubectl get namespaces
```

## Tools
```sh

kubectl apply -f  https://raw.githubusercontent.com/spekt8/spekt8/master/spekt8-deployment.yaml
kubectl apply -f https://raw.githubusercontent.com/spekt8/spekt8/master/fabric8-rbac.yaml
kubectl port-forward deployment/spekt8 3000:3000

OR LOCAL BELOW

kubectl apply -f .\Deploy\K8s\spectk8.yaml
kubectl apply -f .\Deploy\K8s\admin.yaml 
kubectl port-forward deployment/spekt8 3000:3000
```

## Others
```sh
kubectl config view

kubectl get pods -n kube-system

kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/v2.2.0/aio/deploy/recommended.yaml

kubectl apply -f ./Deploy/K8S/setup/service-account.yaml

kubectl apply -f ./Deploy/K8S/setup/role-binding.yaml   

#- gennerate token from belo command

kubectl -n kubernetes-dashboard get secret $(kubectl -n kubernetes-dashboard get sa/sourabhr -o jsonpath="{.secrets[0].name}") -o go-template="{{.data.token | base64decode}}"
```
## Terraform
```sh
terraform plan

terraform apply

terraform apply -target=aws_security_group.my_sg

terraform destroy
```

## Migrations
```sh
dotnet ef migrations add <name>
```

# Refrences

- Redis Setup : https://kubernetes.io/docs/tutorials/configuration/configure-redis-using-configmap/


## License

MIT