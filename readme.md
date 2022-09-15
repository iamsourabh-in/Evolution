# Evolution [![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

### Cloud first approach for building applications


![alt text](https://github.com/iamsourabh-in/Evolution/blob/master/docs/frontpage.png)


# Features and Scope

Evolution is a cloud-enabled, devops-ready practice for any applications, built to be deployed on kubernetes.

- Microservices using Clean Architecture
- Async Communication using Message Queue
- GRPC Communication
- Infrastructure as code
- Service Mesh (Kuma/Istio)
- Container (Podman/docker)
- Terraform

# Prerequisites

To successfully run the app you need to have the followings setup correctly.

- [.NET Core](https://dotnet.microsoft.com/) - Free. Cross-platform. Open source.
A developer platform for building all your apps!
- [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-6.0) - ASP.NET Core to create web apps and services that are fast, secure, cross-platform, and cloud-based
- [Docker](https://www.docker.com/) - OS-level virtualization to deliver software in packages called containers.
- [Redis](https://redis.io/) - The open source, in-memory data store used by millions of developers as a database, cache, streaming engine, and message broker.
- [RabbitMQ](https://www.rabbitmq.com/) - RabbitMQ is one of the most popular open source message brokers.
- [Kubernetes](https://kubernetes.io/) - Kubernetes is an open-source container orchestration system for automating software deployment, scaling, and management. (kind / minikube / EKS / AKS). Kubectl also is required to be setup.
- [Terraform](https://www.terraform.io/) - Terraform is an open-source infrastructure as code software tool created by HashiCorp.

 - **Learn how to spin up your own cluster** [Click here](https://github.com/iamsourabh-in/Evolution/tree/master/Deploy/readme.md) 


# Project Overview

The Project mainly consist of three services which are built on dotnet.


- **Evolution.Identity** - Which controls the identity access for future use.

- **Platform Service** - Which Recieves a request to perform actions on platform the service queues the action for command service to subscribe.

- **Command Service** - The services maintains the command for each platform.



# Architecture


![alt text](https://github.com/iamsourabh-in/Evolution/blob/master/docs/infra.svg)


# Deployment


Learn how to deploy to Kubernetes [Click here](https://github.com/iamsourabh-in/Evolution/tree/master/Deploy/readme.md) 



# Migrations
```sh
dotnet ef migrations add <name>

dotnet update-database
```

# Other Refrences

- Redis Setup : https://kubernetes.io/docs/tutorials/configuration/configure-redis-using-configmap/


## License
[MIT](https://choosealicense.com/licenses/mit/)