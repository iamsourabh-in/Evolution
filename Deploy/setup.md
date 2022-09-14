
# Evolution

### The docs talk about setting up your development environment.


# Install dependencies

## Linux

```sh

#get kubectl
snap install kubectl --classic

# Update the apt package index, and install the latest version of Docker Engine, containerd, and Docker Compose, or go to the next step to install a specific version

#remove older versions
sudo apt-get remove docker docker-engine docker.io containerd runc

sudo apt-get update
sudo apt-get install docker-ce docker-ce-cli containerd.io docker-compose-plugin

```

## Windows

```sh

#todo

```

# Create your own K8 cluster
### We would be using Kind to spin up our new kubenetes cluster.


```sh

curl -Lo ./kind https://kind.sigs.k8s.io/dl/v0.15.0/kind-linux-amd64
chmod +x ./kind
sudo mv ./kind /usr/local/bin/kind


kind create cluster --name kind

kubectl cluster-info -- context kind-kind

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
