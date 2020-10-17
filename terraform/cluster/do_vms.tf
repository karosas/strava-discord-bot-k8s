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
}

provider "kubectl" {
  host = digitalocean_kubernetes_cluster.cyber.kube_config[0].host
  cluster_ca_certificate = base64decode(digitalocean_kubernetes_cluster.cyber.kube_config[0].cluster_ca_certificate)
  token                  = digitalocean_kubernetes_cluster.cyber.kube_config[0].token
  load_config_file       = false
}

resource "kubectl_manifest" "apply_do_specific_ingress_nginx" {
  depends_on = [digitalocean_kubernetes_cluster.cyber]
  yaml_body = file('ingress.yaml')
  
  provisioner "local-exec" {
    command = "curl https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v0.35.0/deploy/static/provider/do/deploy.yaml --output ingress.yaml"
  }
}
