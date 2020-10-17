#######
# k8s #
#######

provider "kubernetes" {
  load_config_file = false
  host = digitalocean_kubernetes_cluster.cyber.endpoint
  token = digitalocean_kubernetes_cluster.cyber.kube_config[0].token

  cluster_ca_certificate = base64decode(
  digitalocean_kubernetes_cluster.cyber.kube_config[0].cluster_ca_certificate
  )
}

resource "kubernetes_ingress" "hello-world" {
  metadata {
    name = "hello-world"
    annotations = {
      "kubernetes.io/ingress.class" = "nginx"
    }
  }

  spec {
    rule {
      host = "echo.edgarasausvicas.com"
      http {
        path {
          backend {
            service_name = "echoserver"
            service_port = 8080
          }

          path = "/"
        }
      }
    }
  }

  # So to have an IP for CNAME to use
  wait_for_load_balancer = true
}

resource "kubernetes_service" "echoserver" {
  metadata {
    name = "echoserver"
  }

  spec {
    selector = {
      app = "echoserver"
    }

    port {
      port = 8080
      target_port = 8080
    }

    type = "ClusterIP"
  }
}

resource "kubernetes_deployment" "echoserver" {
  metadata {
    name = "echoserver"
  }

  spec {
    selector {
      match_labels = {
        app = "echoserver"
      }
    }

    template {
      metadata {
        labels = {
          app = "echoserver"
        }
      }

      spec {
        container {
          name = "echoserver"
          image = "gcr.io/google_containers/echoserver:1.4"

          port {
            container_port = 8080
          }
        }
      }
    }
  }
}


########
# Helm #
########

provider "helm" {
  kubernetes {
    load_config_file = false
    host = digitalocean_kubernetes_cluster.cyber.endpoint
    token = digitalocean_kubernetes_cluster.cyber.kube_config[0].token

    cluster_ca_certificate = base64decode(
    digitalocean_kubernetes_cluster.cyber.kube_config[0].cluster_ca_certificate
    )
  }
}

resource "helm_release" "consul" {
  name = "consul-release"
  repository = "https://helm.releases.hashicorp.com"
  chart = "consul"
  version = "0.24.1"
}

#TODO: Consul