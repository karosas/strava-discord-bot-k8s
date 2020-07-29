#!/bin/sh
set -e

# Install kernel headers with apt
echo "Installing Linux headers using apt"
apt-get -yq install linux-headers-$(uname -r)
exit $?