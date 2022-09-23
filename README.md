# ContactApp
# The project has 2 microservice structures.
- User Microservice
- Report Microservice
The project pipeline is used as user Create -> create Report --> export Report.
Basic crud operations can be done for all microservices. The project uses mongodb as its database. In the project, separate extentions are made for the excel file.

# Module architecture
If I briefly talk about module architecture structures. Application layers are the application layer for the project. In this layer, the classes that go to the requests are hosted. In the Persistence layers, the permanent structures for the project, for example, instead of the interfaces of the service class, are directly hosted and it is the layer that provides the connection with the shared layers.
# Technology Stack
.Net 5.0 , postgress, rabbitmq , MassTransit
