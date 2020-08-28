# DigitalOcean Access Token
variable "token" {
    type = string
}

# SSH keys added to DigitalOcean have names and are easier to retrieve this way
variable "ssh_name" {
  type = string
}

variable "region" {
    type    = string
    default = "fra1"
}

variable "node_count" {
    type    = number
    default = 2
}

variable "apt_packages" {
    type    = list(string)
    default = []
}