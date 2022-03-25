

resource "kubernetes_persistent_volume" "sql-pv" {
  metadata {
    name = "sql-pv"
  }
  spec {
    capacity = {
      storage = "2Gi"
    }
    access_modes = ["ReadWriteOnce"]
    persistent_volume_source {
      host_path {
        path = "/temp/test" 
        type = "DirectoryOrCreate"
      }
    }
    storage_class_name = "hostpath"
  }
}

resource "kubernetes_persistent_volume_claim" "sql-pvc" {
  metadata {
    name = "sql-pvc"
  }
  spec {
    access_modes = ["ReadWriteOnce"]
    volume_name  = kubernetes_persistent_volume.sql-pv.metadata.0.name
    resources {
      requests = {
        storage = "1Gi"
      }
    }
  }
}

resource "kubernetes_deployment" "mssql_tf_depl" {
  metadata {
    name = "mssql-tf-depl"
    labels = {
      app = "mssql-server"
    }
  }
  spec {
    selector {
      match_labels = {
        app = "mssql-server"
      }
    }
    replicas = 2
    template {

      metadata {
        labels = {
          app = "mssql-server"
        }
      }

      spec {
        container {
          name  = "mssql-server"
          image = "mcr.microsoft.com/mssql/server:2017-latest"
          port {
            container_port = 1433
          }
          env {
            name  = "MSSQL_PID"
            value = "Express"

          }
          env {
            name  = "ACCEPT_EULA"
            value = "Y"
          }
          env {
            name = "SA_PASSWORD"
            value_from {
              secret_key_ref {
                name = "mssql"
                key  = "SA_PASSWORD"
              }
            }
          }
          volume_mount {
            mount_path = "/var/opt/mssql/data"
            name       = "mssqldb"
          }
        }
        volume {
          name = "mssqldb"
          persistent_volume_claim {
            claim_name = kubernetes_persistent_volume_claim.sql-pvc.metadata.0.name
          }
        }
      }
    }
  }
}



resource "kubernetes_service" "mssql-cluster-ip" {
  metadata {
    name = "mssql-cluster-ip"
  }
  spec {
    selector = {
      app = kubernetes_deployment.mssql_tf_depl.metadata.0.labels.app
    }
    port {
      name = kubernetes_deployment.mssql_tf_depl.metadata.0.labels.app
      port        = 1433
      target_port = 1433
      protocol = "TCP"
    }

    type = "ClusterIP"
  }
}

resource "kubernetes_service" "mssql-lb" {
  metadata {
    name = "mssql-lb"
  }
  spec {
    selector = {
      app =kubernetes_deployment.mssql_tf_depl.metadata.0.labels.app
    }
    port {
      port        = 1433
      target_port = 1433
      protocol = "TCP"
    }

    type = "LoadBalancer"
  }
}