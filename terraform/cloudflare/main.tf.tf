##############
# CloudFlare #
##############

locals {
  zone_id = lookup(data.cloudflare_zones.domain_zones.zones[0], "id")
}


provider "cloudflare" {
  email = var.email
  api_token = var.api_token
}


data "cloudflare_zones" "domain_zones" {
  filter {
    name = var.domain
    status = "active"
    paused = false
  }
}

resource "cloudflare_record" "cnames" {
  for_each = var.cnames
  
  zone_id = local.zone_id
  name = each.value.name
  value = each.value.value
  type = each.value.type
  proxied = each.value.proxied
}