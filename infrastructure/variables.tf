/* general */
variable "node_count" {
  default = 3
}

/* etcd_node_count must be <= node_count; odd numbers provide quorum */
variable "etcd_node_count" {
  default = 3
}

variable "domain" {
  default = "example.com"
}

variable "hostname_format" {
  default = "kube%d"
}

/* digitalocean */
variable "digitalocean_token" {
  default = ""
}

variable "digitalocean_ssh_keys" {
  type    = list(string)
  default = [""]
}

variable "digitalocean_ssh_name" {
  type    = string
  default = ""
}

variable "digitalocean_region" {
  default = "fra1"
}

variable "digitalocean_size" {
  default = "s-1vcpu-1gb"
}

variable "digitalocean_image" {
  default = "ubuntu-20-04-x64"
}

/* cloudflare dns */
variable "cloudflare_email" {
  default = ""
}

variable "cloudflare_api_token" {
  default = ""
}