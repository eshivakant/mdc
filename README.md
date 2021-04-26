# Market Data Contribution API Gateway (mdc)
### A microservices based .net core API

## Highlights
* Ocelot for API Gateway
* Spring Euereka for service discovery
* Rabbit MQ message bus integration (pending)
* JWT Token authentication
* Blazor Test GUI - can be used to:
  * Process contributions
  * Retrieve contributions
  * Generate API Key that can be used bu other clients to talk to the API Gateway

## Starting 
For starting all services manually, please ensure to run Euerka using following docker command first:
> docker run --publish 8761:8761 steeltoeoss/eureka-server![image](https://user-images.githubusercontent.com/10363700/116032544-d02e6880-a657-11eb-97d0-ab61663c1c62.png)

After starting Eureka, start all services. Docker Launch configs are not ready yet, so please use project launch config to launch.

### Services
![Screenshot](doc/services.PNG)

## Service Discovery
![Screenshot](doc/servicediscovery.PNG)

## Authenticate in GUI
![Screenshot](doc/web0.PNG)

## Contributions
Real time messages from all services are received by the GUI. See validation messages on current and previous tries in the console window.
![Screenshot](doc/web1.PNG)

## Fetch Contributions
More work need to be done here - e.g. querying etc
![Screenshot](doc/web2.PNG)

## API Key generation for other clients
![Screenshot](doc/web3.PNG)

