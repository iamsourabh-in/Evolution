resource "kubernetes_deployment" "helloworld" {
  metadata {
    name = "platforms-tf-depl"
  }
  spec {
    selector {
      match_labels = {
        "app" = "platformservice"
      }
    }
    replicas = 2
    template {

      metadata {
        labels = {
          "app" = "platformservice"
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
