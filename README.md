## How to run this ? 
The .jar and .exe files necessary to run this project without hassle are provided in ./Runnables
They could also be run manually by following the instructions :

## **This project has two parts**
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