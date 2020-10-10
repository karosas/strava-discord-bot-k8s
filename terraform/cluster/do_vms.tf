provider "digitalocean" {
  token = var.token
}

resource "digitalocean_kubernetes_cluster" "cyber" {
  name = "cyber"
  region = var.region

  version = "1.18.8-do.1"

  node_pool {
    name = "worker-pool"
    size = "s-1vcpu-2gb"
    node_count = var.node_count
  }

  provisioner "remote-exec" {
    inline = [
      "curl -LO \"https://storage.googleapis.com/kubernetes-release/release/$(curl -s https://storage.googleapis.com/kubernetes-release/release/stable.txt)/bin/linux/amd64/kubectl\"",
      "chmod 0755 kubectl",
      "./kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v0.35.0/deploy/static/provider/do/deploy.yaml"
    ]
  }
}