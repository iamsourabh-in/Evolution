# Create you own K8 cluster to work with in local.

### The docs talk about setting up your development environment.



# Installations

### 1. Install `docker` to work with kind
```sh


#Install Docker using the repository
#Before you install Docker Engine for the first time on a new host machine, you need to set up the Docker repository. Afterward, you can install and update Docker from the repository.

#Set up the repository
#Update the apt package index and install packages to allow apt to use a repository over HTTPS:

 sudo apt-get update
 sudo apt-get install \
    ca-certificates \
    curl \
    gnupg \
    lsb-release

#Add Docker’s official GPG key:

 sudo mkdir -p /etc/apt/keyrings
 curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg
Use the following command to set up the repository:

 echo \
  "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu \
  $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
#Install Docker Engine
#Update the apt package index, and install the latest version of Docker Engine, containerd, and Docker Compose, or go to the next step to install a specific version:

 sudo apt-get update
 sudo apt-get install docker-ce docker-ce-cli containerd.io docker-compose-plugin

# Update the apt package index, and install the latest version of Docker Engine, containerd, and Docker Compose, or go to the next step to install a specific version

#remove older versions
sudo apt-get remove docker docker-engine docker.io containerd runc

sudo apt-get update
sudo apt-get install docker-ce docker-ce-cli containerd.io docker-compose-plugin

```

## Create your own K8 cluster
### 2. We would be using `Kind` to spin up our new kubenetes cluster.


```sh

# Get kind and add to path
curl -Lo ./kind https://kind.sigs.k8s.io/dl/v0.15.0/kind-linux-amd64
chmod +x ./kind
sudo mv ./kind /usr/local/bin/kind


kind create cluster --name kind

```

### 3. Get Kubectl to Interact with the cluster

```sh

#Get kubectl
snap install kubectl --classic

# --- OR ---


#Get latest stable release
curl -LO "https://dl.k8s.io/release/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl"

# Note: If you do not have root access on the target system, you can still install kubectl to the ~/.local/bin directory:
chmod +x kubectl
mkdir -p ~/.local/bin
mv ./kubectl ~/.local/bin/kubectl
# and then append (or prepend) ~/.local/bin to $PATH


kubectl version --client --output=yaml    #client version

kubevtl version #server + client

# Verify to be able to talk to cluster
kubectl cluster-info -- context kind-kind

kubectl get pods -n 

```


### When working with Kuma

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


# Working with FLux

```sh

kubectl create ns flux


fluxctl install --git-user=iamsourabh-in --git-email=iamsourabh-in@users.noreply.github.com --git-url=git@github.com:iamsourabh-in/Evolution --git-path=Deploy/K8s/Kustomize/overlays/dev --git-branch=reorganize --namespace=flux-system | kubectl apply -f -

kubectl -n flux-system rollout status deployment/flux

fluxctl list-workloads --k8s-fwd-ns flux-system

fluxctl identity --k8s-fwd-ns flux-system


https://github.com/marcel-dempers/docker-development-youtube-series/settings/keys/new

fluxctl sync --k8s-fwd-ns flux-system

  annotations:
    fluxcd.io/tag.example-app: semver:~1.0
    fluxcd.io/automated: 'true'

fluxctl policy -w default:deployment/example-deploy --tag "example-app=1.0.*"

```