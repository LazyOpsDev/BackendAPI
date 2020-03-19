#!/bin/bash

# Check if services are up and running:
# - fails when docker swarm isn't configured,
# - fails when docker services are not running
docker service ls | grep minitwit
if [ $? -eq 0 ]; then
    echo "Apply rolling updates"
    docker service update \
        --image lazyopsdev/api:latest \
        --update-parallelism 1 \
        --update-delay 10s \
        minitwit_minitwit

# If services are not yet running, bring them up:
else
    echo "Deploy all services if not running"
    docker stack deploy -c docker-compose.yml minitwit
fi
