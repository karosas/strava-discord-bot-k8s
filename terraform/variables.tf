# DigitalOcean Access Token
variable "do_token" {
    type = string
}

variable "cf_token" {
    type = string
}

variable "domain" {
    type = string
}

variable "cf_email" {
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