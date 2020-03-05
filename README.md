# BackendAPI
[![Build Status](https://travis-ci.org/LazyOpsDev/Minitwit.Backend.svg?branch=develop)](https://travis-ci.org/LazyOpsDev/Minitwit.Backend)<br><br>
BackendAPI for Minitwit

## Deployment
ssh into droplet

#### pull image:
```
docker image pull lazyopsdev/api:latest
```

#### deploy on swarm:
```
docker service update --update-parallelism 1 --update-delay 10s -- minitwit_minitwit
```
