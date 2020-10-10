output "ingress_lb_ip" {
  value = kubernetes_ingress.hello-world.load_balancer_ingress[0].ip
}