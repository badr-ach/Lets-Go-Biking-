# Let's Go Biking
## By Badr Al Achkar and Louis Calas

### The goal of the Let’s Go Biking! project is to develop a small app to compute itineraries by reducing as much as possible the distance tocover by foot (by using bikes instead)

### External Resources

#### One of JC Decaux’s business is renting bikes. They have stations in many cities where anyone can get or drop a bike (like Vélo bleu), and they developed an Open API offering real- time information about their stations.

#### OpenStreetMap is an open map project, offering services such as GPS itineraries.

## How to run this project ?
The .jar and .exe files necessary to run this project without hassle are provided in ./Runnables
They could also be run manually by following the instructions down below :

### **This project has two parts**
### First part that should be run is the back-end :
- Start Activemq on your machine
- Head over to the directory Routing Server
- Open the project solution RoutingServer.sln as **administrator** (privilege needed to create the services)
- Run the project ProxyServer_Host first
- Once the host is deployed successfully close it (the .exe is generated)
- Run the project Routing_Server_Host second
- Once the host is deployed succesfully close it too (again the .exe is generated)

Both .exe files can be found in *ProxyServer_Host\bin\Debug* and *Routing_Server_Host\bin\Debug* (respectfully)
Run them both in the same order.

### Second part that should be run is the front end :

As the front end is made using swing in netbeans, it can only be opened with netbeans (latest version works too)
(intellij seems to have trouble finding certain methods)
- Open the project in netbeans
- Right click the project name, and choose build with dependencies
- in the target folder the .jar will be loaded

Run the generated .jar file (it is also possible to just right click the Home.class and run file)

Now it should be possible to start the exchange between the front-end and back-end.

- Fill in the two addresses and press Find to query for the itinerary (this version uses a proxy only)
- Fill in the two addresses and press Directions to query for the itinerary step by step, press next to get the following step
(this version uses active mq)
Note : the activemq version creates a topic called APP_QUEUE that should be deleted in case of incompletion of the trajectory 
Otherwise the next itinerary queries (with the second version) will return the first old steps that were queue'd.
