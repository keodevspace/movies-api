# How You Can Create This API

This tutorial will guide you from project creation to implementing basic functionalities using .NET Core and Entity Framework Core (EF Core).

## 1) Create the Project
### Install the .NET SDK:

Before you start, make sure you have the .NET SDK installed. You can download it [here](https://dotnet.microsoft.com/download/dotnet).

### Create a new API project:

- Open the terminal or command prompt.
- Run the following command to create a new API project:
  ```bash
  dotnet new webapi -n MoviesAPI

Navigate to the project folder:
 ```bash
cd MoviesAPI
```

2) Set Up the Database with EF Core
Add EF Core to the project:
Install the necessary packages for Entity Framework Core with SQL Server:
````bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
````

3) Create the Movie class (Model):
Inside the Models folder, create a Movie.cs file.

4) Create the DbContext:
Create a folder named Data and add the MoviesContext.cs class.

5) Configure the connection string:
In the appsettings.json file, add the connection string for the database.

6) Register the DbContext in Program.cs

7) Create the Controller
Add the Controller for Movies:
Create a folder named Controllers and add the MoviesController.cs class.

8) Set Up Migrations and Update the Database
````bash
dotnet ef migrations add InitialCreate
dotnet ef database update
````

9) Test the API
Run the application and test endpoints

10) Optional: Document the API with Swagger
Add the Swagger package:
Install the Swagger documentation package.
````bash
dotnet add package Swashbuckle.AspNetCore
````

11) Configure Swagger in Program.cs:
Add the necessary configurations for Swagger.


## Access the documentation:
After running the application, go to https://localhost:{port}/swagger to view the API documentation.