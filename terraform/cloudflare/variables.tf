variable "email" {
  type = string
}

variable "api_token" {
  type = string
}

variable "domain" {
  type = string
}

variable "cnames" {
  type = map(
    object({
      name = string
      value = string
      type = string
      proxied = bool
    })
  )
}