terraform {
  required_providers {
    mycloud = {
      source  = "hashicorp/kubernetes"
      version = "~> 1.13"
    }
  }
  backend "local" {
    path = "/tmp/terraform.tfstate"
  }
}

provider "kubernetes" {
  host = "https://kubernetes.docker.internal:6443"
  config_path = "C:\\Users\\Sourabh\\.kube\\config"
}
