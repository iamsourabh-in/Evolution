resource "kubernetes_deployment" "commandservice" {
  metadata {
    name = "command-tf-depl"
    labels = {
      app = "commandservice"
    }
  }
  spec {
    selector {
      match_labels = {
        app = "commandservice"
      }
    }
    replicas = 1
    template {

      metadata {
        labels = {
          app = "commandservice"
        }
      }

      spec {
        container {
          name  = "commandservice"
          image = "rohitrustagi007/commandservice"
          port {
            container_port = 80
          }
        }
      }

    }
  }
}

resource "kubernetes_service" "command-cluster-ip" {
  metadata {
    name = "command-cluster-ip"
  }
  spec {
    selector = {
      app = kubernetes_deployment.commandservice.metadata.0.labels.app
    }
    port {
      name = kubernetes_deployment.commandservice.metadata.0.labels.app
      port        = 80
      target_port = 80
      protocol = "TCP"
    }

    type = "ClusterIP"
  }
}

resource "kubernetes_service" "command-node-port" {
  metadata {
    name = "command-node-port"
  }
  spec {
    selector = {
      app = kubernetes_deployment.commandservice.metadata.0.labels.app
    }
    port {
      name = kubernetes_deployment.commandservice.metadata.0.labels.app
      port        = 80
      target_port = 80
      protocol = "TCP"
    }

    type = "NodePort"
  }
}