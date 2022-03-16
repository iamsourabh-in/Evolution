
# Docker

docker build -t rohitrustagi007/platformservice .

docker build -t rohitrustagi007/commandservice .

docker push rohitrustagi007/platformservice

docker push rohitrustagi007/commandservice


# Kubenetes

kubectl


kubectl get pods

kubectl version 

kubectl apply -f K8s/platform-depl.yaml


kubectl apply -f K8s/command-depl.yaml

 kubectl describe pod platforms-depl-85c65658cd-44zc8  

 docker info

 kubectl get services