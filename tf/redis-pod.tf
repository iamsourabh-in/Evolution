resource "kubernetes_pod" "redis-pod" {
  metadata {
    name = "redis"
  }
  spec {
    container {
      name    = "redis"
      image   = "redis:5.0.4"
      command = ["redis-server", "/redis-master/redis.conf"]
      env {
        name  = "MASTER"
        value = "true"
      }
      port {
        container_port = 6379
      }
      resources {
        limits {
          cpu = "0.1"
        }
      }



      volume_mount {
        mount_path = "/redis-master-data"
        name       = "data"
      }
      volume_mount {
        mount_path = "/redis-master"
        name       = "config"
      }


      #   liveness_probe {
      #     http_get {
      #       path = "/"
      #       port = 80

      #       http_header {
      #         name  = "X-Custom-Header"
      #         value = "Awesome"
      #       }
      #     }

      #     initial_delay_seconds = 3
      #     period_seconds        = 3
      #   }

    }

    volume {
      name = "data"
      empty_dir {

      }
    }
    volume {
      name = "config"
      config_map {
        name = "redis-config"
        items {
          key  = "redis-config"
          path = "redis.conf"
        }
      }
    }


  }
}
