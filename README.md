# Api_AspnetCore_Ef_3
Balta.io classes 

# Docker setup WIP

In this project i used docker for the SQL Server.
You can find the image and usage here https://hub.docker.com/_/microsoft-mssql-server

Or 

Just run this command in the terminal
docker pull mcr.microsoft.com/mssql/server

To start your image, just run the next command
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=1q2w3e4r!@#$' -p 1433:1433 -d mcr.microsoft.com/mssql/server

# Project setup WIP

Database in memory
services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));

Database in SQL
services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("connectionString")));

Entity framework tool global installation
dotnet tool install --global dotnet-ef

# How to run

wip

# How to execute migrations

wip