# CommandService
Command microservice

## Build the docker image
```powershell
$version="0.0.6"
docker build -t command.service:$version .
```

## Run the docker image on local machine
```powershell
docker run -it --rm -p 5265:5265 --name commandservice command.service:$version
```

## Push the docker to Local Container Registry
```powershell

$crname="container-registry.docker.local:5000"
docker tag command.service:$version "$crname/command.service:$version"

docker push "$crname/command.service:$version"
```

## Deploy to kubernetes
```powershell

$namespace="platform"

kubectl apply -f .\kubernetes\deployment.yaml -n $namespace

kubectl apply -f .\kubernetes\cluster-ip-service.yaml -n $namespace

# list pods in namespace
kubectl get pods -n $namespace -w

# list services in namespace
kubectl get services -n $namespace
``` 