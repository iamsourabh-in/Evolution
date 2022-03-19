
# Docker

docker build -t rohitrustagi007/platformservice .

docker push rohitrustagi007/platformservice



docker build -t rohitrustagi007/commandservice .

docker push rohitrustagi007/commandservice


# Kubenetes


## Deployments
kubectl apply -f K8s/platform-depl.yaml

kubectl delete deploy platforms-depl 


kubectl apply -f K8s/command-depl.yaml

kubectl delete deploy command-depl 

## Ingress

kubectl apply -f .\K8s\ingress-nginx-srv.yaml

kubectl delete ingress ingress-nginx-srv 

## Persistance volume claims

kubectl apply -f .\K8s\local-pvc.yaml

kubectl delete pvc local-pvc

## Secrets

kubectl create secret generic mssql --from-literal=SA_PASSWORD="password@1"



## MSSQL Server Express 


kubectl apply -f .\K8s\mssql-depl.yaml

kubectl delete deploy mssql-depl



# Helpers

kubectl describe pod platforms-depl-85c65658cd-44zc8  

docker info

kubectl get services

kubectl get pvc  -o wide

kubectl get pods

kubectl get namespaces