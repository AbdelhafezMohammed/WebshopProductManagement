
# WebShopProductManagement

# How run the solution:

 **Start backend using Dotnet CLI:**
 
 1- Navigate to the solution root folder
 
 2- Using Terminal or CMD run command `dotnet restore` to retore nuget packages
 
 3- Navigate to backend\Webshop.API
 
 4- Using Terminal or CMD run command `dotnet run`

 **Start backend using  Visual Studio:**
 
 1-Open the solution file using Visual Studio
 
 2-Build the solution that will automatically restore nuget packages
 
 3-Run the project using F5 or from Solution Explorer right click on Webshop.API >Debug>Start New Instance

 **Start frontend:**
 
 1- Navigate to frontend\WebShop.Web
 
 2- Terminal or CMD run command `npm install`
 
 2- run commange `npm start`

 **Start Database:**
 
 - using docker
   
 1- Navigate to the solution root folder
 2-  Using terminal or cmd run command `docker compose up`
 
 -Local SQL Server
1- Install SQL server locally and set password for sql server `sa` user from `SqlServerConnectionString` in appsettings.json in WebShop.API 


 **How to test:**
 
- Use your browser to open http://localhost:5150/swagger/index.html for API documentation and testing

- Use your browser to open http://localhost:3000/ for front-end

# Technology stack

Backend: C#, Asp.Net WebAPI, Entity Framework , SQL Server

Frontend: React, Material UI


# Tooling

VSCode, Visual Studio
SQL Server
Docker
Dotnet SDK V 7.0.405
NPM
Dotnet CLI


 

