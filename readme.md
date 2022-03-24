# Evolution 

Could-first

# Docker

docker build -t rohitrustagi007/platformservice .

docker push rohitrustagi007/platformservice



docker build -t rohitrustagi007/commandservice .

docker push rohitrustagi007/commandservice


# Kubenetes


## Deployments
kubectl apply -f K8s/platform-depl.yaml

kubectl delete deploy platforms-depl 

kubectl delete service platform-cluster-ip



kubectl apply -f K8s/command-depl.yaml

kubectl delete deploy command-depl 

kubectl delete service command-cluster-ip

## Ingress

kubectl apply -f .\K8s\ingress-nginx-srv.yaml

kubectl delete ingress ingress-nginx-srv 

## Persistance volume claims

kubectl apply -f .\K8s\local-pvc.yaml

kubectl delete pvc sqlserver-pvc

## Secrets

kubectl create secret generic mssql --from-literal=SA_PASSWORD="password@1"



## MSSQL Server Express 


kubectl apply -f .\K8s\mssql-depl.yaml

kubectl delete deploy mssql-depl

kubectl delete service mssql-loadbalancer

kubectl delete service mssql-cluster-ip

# Helpers

kubectl describe pod platforms-depl-85c65658cd-44zc8  

docker info

kubectl get services

kubectl get pvc  -o wide

kubectl get pods

kubectl get namespaces


## Tools


kubectl apply -f  https://raw.githubusercontent.com/spekt8/spekt8/master/spekt8-deployment.yaml
kubectl apply -f https://raw.githubusercontent.com/spekt8/spekt8/master/fabric8-rbac.yaml
kubectl port-forward deployment/spekt8 3000:3000

OR LOCAL BELOW

kubectl apply -f .\k8s\spectk8.yaml
kubectl apply -f .\k8s\admin.yaml 
kubectl port-forward deployment/spekt8 3000:3000


## Others

kubectl config view

kubectl get pods -n kube-system

kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/v2.2.0/aio/deploy/recommended.yaml

kubectl apply -f ./K8S/setup/service-account.yaml

kubectl apply -f ./K8S/setup/role-binding.yaml   

kubectl -n kubernetes-dashboard get secret $(kubectl -n kubernetes-dashboard get sa/sourabhr -o jsonpath="{.secrets[0].name}") -o go-template="{{.data.token | base64decode}}"

## Terraform

terraform plan

terraform apply

terraform destroy

## minikube
