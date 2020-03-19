# BackendAPI
[![Build Status](https://travis-ci.org/LazyOpsDev/Minitwit.Backend.svg?branch=develop)](https://travis-ci.org/LazyOpsDev/Minitwit.Backend) ![deployment](https://img.shields.io/circleci/build/github/LazyOpsDev/Minitwit.Backend/develop?label=deployment&token=e5c7edeb487cf79539ffe2bf2f6bd5ecba0b0eb6) [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=LazyOpsDev_Minitwit.Backend&metric=alert_status)](https://sonarcloud.io/dashboard?id=LazyOpsDev_Minitwit.Backend) ![release](https://img.shields.io/github/v/tag/LazyOpsDev/Minitwit.Backend?style=flat)

<br>

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
