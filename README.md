# Market Data Contribution API Gateway (mdc)
## A microservices based .net core API

* Ocelot for API Gateway
* Spring Euereka for service discovery
* Rabbit MQ message bus integration (pending)
* JWT Token authentication
* Blazor Test GUI - can be used to:
  * Process contributions
  * Retrieve contributions
  * Generate API Key that can be used bu other clients to talk to the API Gateway
 
### Services
![Screenshot](doc/services.PNG)

## Service Discovery
![Screenshot](doc/serviceDiscovery.PNG)

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

