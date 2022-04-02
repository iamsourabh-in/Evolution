resource "kubernetes_deployment" "rabbitmqserver" {
  metadata {
    name = "rabbitmqserver"
    labels = {
      app = "rabbitmq"
    }
  }
  spec {
    selector {
      match_labels = {
        app = "rabbitmq"
      }
    }
    replicas = 1
    template {

      metadata {
        labels = {
          app = "rabbitmq"
        }
      }

      spec {
        container {
          name  = "rabbitmq"
          image = "rabbitmq:3-management"
          port {
            container_port = 15672
            name           = "rbmq-mgmt-port"
          }
          port {
            name           = "rbmq-msg-port"
            container_port = 5672
          }
        }
      }

    }
  }
}


resource "kubernetes_service" "rabbitmq-cluster-ip" {
  metadata {
    name = "rabbitmq-cluster-ip"
  }
  spec {
    selector = {
      app = kubernetes_deployment.rabbitmqserver.metadata.0.labels.app
    }
    port {
      name        = "rbmq-mgmt-port"
      port        = 15672
      target_port = 15672
      protocol    = "TCP"
    }
    port {
      name        = "rbmq-msg-port"
      port        = 5672
      target_port = 5672
      protocol    = "TCP"
    }
    type = "ClusterIP"
  }
}


resource "kubernetes_service" "rabbitmqserver-lb" {
  metadata {
    name = kubernetes_deployment.rabbitmqserver.metadata.0.labels.app
  }
  spec {
    selector = {
      app = kubernetes_deployment.rabbitmqserver.metadata.0.labels.app
    }
    port {
      name        = "rbmq-msg-portlb"
      port        = 5672
      target_port = 5672
      protocol    = "TCP"
    }
    port {
      name        = "rbmq-mgmt-port-lb"
      port        = 15672
      target_port = 15672
      protocol    = "TCP"
    }
    type = "LoadBalancer"
  }
}
