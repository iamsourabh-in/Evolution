resource "kubernetes_deployment" "platformservice" {
  metadata {
    name = "platforms-tf-depl"
    labels = {
      app = "platformservice"
    }
  }
  spec {
    selector {
      match_labels = {
        app = "platformservice"
      }
    }
    replicas = 2
    template {

      metadata {
        labels = {
          app = "platformservice"
        }
      }

      spec {
        container {
          name  = "platformservice"
          image = "rohitrustagi007/platformservice"
          port {
            container_port = 80
          }
        }
      }

    }
  }
}

resource "kubernetes_service" "example" {
  metadata {
    name = "platform-cluster-ip"
  }
  spec {
    selector = {
      app = kubernetes_deployment.platformservice.metadata.0.labels.app
    }
    port {
      name = kubernetes_deployment.platformservice.metadata.0.labels.app
      port        = 80
      target_port = 80
      protocol = "TCP"
    }

    type = "ClusterIP"
  }
}

resource "kubernetes_service" "platform-node-port" {
  metadata {
    name = "platformservice-srv"
  }
  spec {
    selector = {
      app = kubernetes_deployment.platformservice.metadata.0.labels.app
    }
    port {
      name = kubernetes_deployment.platformservice.metadata.0.labels.app
      port        = 80
      target_port = 80
      protocol = "TCP"
    }

    type = "NodePort"
  }
}