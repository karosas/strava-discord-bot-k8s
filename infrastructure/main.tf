module "provider" {
  source = "github.com/hobby-kube/provisioning/provider/digitalocean"

  token           = var.digitalocean_token
  ssh_keys        = var.digitalocean_ssh_keys
  region          = var.digitalocean_region
  size            = var.digitalocean_size
  image           = var.digitalocean_image
  hosts           = var.node_count
  hostname_format = var.hostname_format
}

module "swap" {
  source = "github.com/hobby-kube/provisioning/service/swap"

  node_count  = var.node_count
  connections = module.provider.public_ips
}

module "dns" {
  source = "github.com/hobby-kube/provisioning/dns/cloudflare"

  node_count = var.node_count
  email      = var.cloudflare_email
  api_token  = var.cloudflare_api_token
  domain     = var.domain
  public_ips = module.provider.public_ips
  hostnames  = module.provider.hostnames
}

module "wireguard" {
  source = "github.com/hobby-kube/provisioning/security/wireguard"

  node_count   = var.node_count
  connections  = module.provider.public_ips
  private_ips  = module.provider.private_ips
  hostnames    = module.provider.hostnames
  overlay_cidr = module.kubernetes.overlay_cidr
}

module "firewall" {
  source = "github.com/hobby-kube/provisioning/security/ufw"

  node_count           = var.node_count
  connections          = module.provider.public_ips
  private_interface    = module.provider.private_network_interface
  vpn_interface        = module.wireguard.vpn_interface
  vpn_port             = module.wireguard.vpn_port
  kubernetes_interface = module.kubernetes.overlay_interface
}

module "etcd" {
  source = "github.com/hobby-kube/provisioning/service/etcd"

  node_count  = var.etcd_node_count
  connections = module.provider.public_ips
  hostnames   = module.provider.hostnames
  vpn_unit    = module.wireguard.vpn_unit
  vpn_ips     = module.wireguard.vpn_ips
}

module "kubernetes" {
  source = "github.com/hobby-kube/provisioning/service/kubernetes"

  node_count     = var.node_count
  connections    = module.provider.public_ips
  cluster_name   = var.domain
  vpn_interface  = module.wireguard.vpn_interface
  vpn_ips        = module.wireguard.vpn_ips
  etcd_endpoints = module.etcd.endpoints
}
