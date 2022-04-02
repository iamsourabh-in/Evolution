resource "kubernetes_ingress" "ingress-nginx-srv" {
  metadata {
    name = "ingress-nginx-srv"
    annotations = {
      "kubernetes.io/ingress.class" : "nginx"
      "nginx.ingress.kubernetes.io/use-regex" = "true"
    }
    labels = {
      "name" : "myingress"
    }
  }

  spec {
    rule {
      host = "acme.com"
      http {
        path {
          backend {
            service_name = "platform-cluster-ip"
            service_port = 80
          }

          path = "/api/platforms"
        }

        path {

          backend {
            service_name = "command-cluster-ip"
            service_port = 80
          }

          path = "/api/c/platforms"
        }
      }
    }
  }
}
