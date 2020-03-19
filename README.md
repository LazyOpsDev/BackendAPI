# BackendAPI
[![Build Status](https://travis-ci.org/LazyOpsDev/Minitwit.Backend.svg?branch=develop)](https://travis-ci.org/LazyOpsDev/Minitwit.Backend) [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=LazyOpsDev_Minitwit.Backend&metric=alert_status)](https://sonarcloud.io/dashboard?id=LazyOpsDev_Minitwit.Backend)

<br><br>

BackendAPI for Minitwit

## Deployment
ssh into droplet

#### pull image:
```
docker image pull lazyopsdev/api:latest
```

#### deploy on swarm:
```
docker service update --image lazyopsdev/api:latest --update-parallelism 1 --update-delay 10s minitwit_minitwit
```
