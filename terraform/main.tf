module "main_cluster" {
  source = "./cluster"

  node_count = var.node_count
  region = var.region
  token = var.do_token
}

module "cloudflare_dns" {
  source = "./cloudflare"

  api_token = var.cf_token
  cname_value = module.main_cluster.ingress_lb_ip
  domain = var.domain
  email = var.cf_email
}