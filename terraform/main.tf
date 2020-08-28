#######
# VMs #
#######

provider "digitalocean" {
  token = var.token
}

resource "digitalocean_kubernetes_cluster" "cyber" {
    name            = "cyber"
    region          = var.region

    version         = "1.18.6-do.0"

    node_pool {
        name        = "worker-pool"
        size        = "s-1vcpu-2gb"
        node_count  = var.node_count
    }

    # Immediately apply DO-specifc ingress-nginx, since for some reason helm fails doing that.
    provisioner "local-exec" {
        command = "kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v0.35.0/deploy/static/provider/do/deploy.yaml"
    }
}

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


##############
# CloudFlare #
##############

locals {
  zone_id = "${lookup(data.cloudflare_zones.domain_zones.zones[0], "id")}"
}

variable "email" {}

variable "api_token" {}

variable "domain" {}


provider "cloudflare" {
  email     = var.email
  api_token = var.api_token
}


data "cloudflare_zones" "domain_zones" {
  filter {
    name   = var.domain
    status = "active"
    paused = false
  }
}

resource "cloudflare_record" "echoserver" {
    zone_id = local.zone_id
    name = "echo"
    value = kubernetes_ingress.hello-world.load_balancer_ingress[0].ip
    type = "A"
    proxied = true
}