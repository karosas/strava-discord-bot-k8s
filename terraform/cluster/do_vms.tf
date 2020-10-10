provider "digitalocean" {
  token = var.token
}

resource "digitalocean_kubernetes_cluster" "cyber" {
  name = "cyber"
  region = var.region

  version = "1.18.6-do.0"

  node_pool {
    name = "worker-pool"
    size = "s-1vcpu-2gb"
    node_count = var.node_count
  }

  # Immediately apply DO-specifc ingress-nginx, since for some reason helm fails doing that.
  provisioner "local-exec" {
    command = "kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v0.35.0/deploy/static/provider/do/deploy.yaml"
  }
}