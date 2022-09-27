# ContactApp
# The project has 2 microservice structures.
- User Microservice
- Report Microservice
The project pipeline is used as user Create -> create Report --> export Report.
Basic crud operations can be done for all microservices. The project uses postgress as its database. In the project, separate extentions are made for the excel file.

# Module architecture
If I briefly talk about module architecture structures. Application layers are the application layer for the project. In this layer, the classes that go to the requests are hosted. In the Persistence layers, the permanent structures for the project, for example, instead of the interfaces of the service class, are directly hosted and it is the layer that provides the connection with the shared layers.

# Database

Postgresql is used as database. You can edit appsetting.json file in user and report webapi projects for connection with Postgresql project.

<code>  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=DbUser;User ID=postgres;Password=123456;"
  }, </code>

  As an open source object-relational database management system, PostgreSQL available for MacOS, Linux, and Windows.

The goal will be to run the following command successfully from the command line (regardless of the OS):

```psql -U postgres```

This should open the psql interactive shell and print a prompt that looks like:

```postgres=# ```

# Install PostgreSQL
### Linux:

1) Acquire the source code: `wget ftp://ftp.postgresql.org/pub/source/v9.3.2/postgresql-9.3.2.tar.bz2`
2) Install the packages needed for building Postgres:
   `sudo apt-get install build-essential libreadline-dev zlib1g-dev flex bison libxml2-dev libxslt-dev libssl-dev`

### Windows:
1) Download the installer specified by EnterpriseDB for all supported PostgreSQL versions. The installer is available here:
  https://www.postgresql.org/download/windows/

# Install Rabbitmq

Go to https://www.rabbitmq.com/install-windows.html and download & run rabbitmq-server-3.8.9.exe

See also Using chocolatey:

<code>Set-ExecutionPolicy Bypass -Scope Process -Force; iex ((New-Object System.Net.WebClient).DownloadString(‘https://chocolatey.org/install.ps1'))</code>

<code> choco install rabbitmq  </code>

Open new cmd and type

cd C:\Program Files\RabbitMQ Server\rabbitmq_server-{version}\sbin

cd C:\Program Files\RabbitMQ Server\rabbitmq_server-3.8.9\sbin

<code > rabbitmq-plugins.bat enable rabbitmq_management</code>

<code> rabbitmq-plugins enable rabbitmq_shovel rabbitmq_shovel_management </code>

Once all done open : http://localhost:15672/ for opening the rabbitmq management.

default user information:

Username: guest
Password: guest

# EndPoint
User Service Swagger:  https://localhost:44348/swagger/index.html

Report Service Swagger:  https://localhost:44397/swagger/index.html

# Installion and Using

The project was written in dotnet 5.0, so you should download it from the link below.

https://dotnet.microsoft.com/en-us/download/dotnet/5.0

For the project to work you have to download other tools postgres and rabbitmq these are described above.

Now, let's get the projects up and running, you can easily do this on visual studio with run tools.

Let's open 3 terminals.

1. Terminal Localation Path:..\ContactApp\ContactApp.Module.User.WebApi
3. Terminal Localation Path:..\ContactApp\ContactApp.Module.Report.WebApi
3. Terminal Localation Path:..\ContactApp\ContactApp.Core.ApiGateway

Let's run the following dotnet commands in all terminals, respectively.

`dotnet build `

`dotnet run `

# Technology Stack
.Net 5.0 , postgress, rabbitmq , MassTransit