##############
# CloudFlare #
##############

locals {
  zone_id = lookup(data.cloudflare_zones.domain_zones.zones[0], "id")
}


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
    value = var.cname_value
    type = "A"
    proxied = true
}