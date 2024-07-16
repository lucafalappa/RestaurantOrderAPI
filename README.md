# RestaurantOrderAPI Project

The project proposes the creation of a web API that allows you to place orders at a restaurant. An order is generally composed of dishes (first course, second course, side dish, dessert), and provides a discount of 10% in case the order is a complete meal (i.e., includes all the types of courses mentioned above).

The backend of the application was developed using the ASP.NET framework, utilizing its web module for the development of REST APIs. As for data persistence, the provided web service relies on Entity Framework Core (EF Core), an Object-Relational Mapping (ORM) tool, to communicate with the underlying SQL Server database.

## Preliminary preparations (database initialization) - SQL Server Management Studio (SSMS)

Both the BACPAC file and the dump.sql file are located inside the folder "RestaurantOrderAPI.Resources".

### Importing database from a BACPAC file ("RestaurantOrderAPI.DBBackup.bacpac")

1. **Connect to SQL Server with the Target Login:**
   - Connect to your SQL Server instance using SQL Server Authentication or Windows Authentication, depending on your setup. Ensure that the used login has sufficient permissions to create users and grant permissions.
2. **Import the BACPAC File:**
   - Right-click on “Databases” in Object Explorer.
   - Select "Import Data-tier Application…".
   - Follow the wizard to import the BACPAC file.
   - Set "New database name" as "RestaurantOrderDB" (MANDATORY).
   - Click "Finish".
3. **Database User Creation:**
   - During the import process, SQL Server Management Studio will create a database user mapped to the SQL Server login used for the import. This user will typically have permissions defined in the BACPAC file.

### Importing from a dump.sql file ("RestaurantOrderAPI.DBDump.sql")

1. **Connect to SQL Server with the Target Login:**
   - Connect to your SQL Server instance using SQL Server Authentication or Windows Authentication, depending on your setup. Ensure that the used login has sufficient permissions to create users and grant permissions.
2. **Execute the SQL script:**
   - Click "File" > "Open" > "File…".
   - Select the appropriate file.
   - Click "Execute".

## Project settings

### Changing the server name (appsettings.Development.json)

1. **Locate the file:**
   - Find the "appsettings.Development.json" file in the project directory.
2. **Open the File:**
   - Open "appsettings.Development.json" in a text editor of your choice.
3. **Change the Server Name:**
   - Replace `<placeservernamehere>` with your SQL Server name. Make sure to keep the rest of the connection string intact.

   ```json
   "ConnectionStrings": {
       "RestaurantOrderAPIDbContext": "Server=<placeservernamehere>;Database=RestaurantOrderDB;User Id=RestaurantOrderLogin;Password=Mypassword01!;MultipleActiveResultSets=True;TrustServerCertificate=True;Integrated Security=True"
     },
   
4. **Save the file:**
   - Save your changes and close the file.
   
## Application startup

Launch the web application directly from Visual Studio, making sure to set the startup project as “RestaurantOrderAPI.WebAPI” (the debugging profile turns out to be “RestaurantOrder-Dev”).

## How to authenticate

To perform the authentication, after the registration or login operations, it is necessary to copy the provided token within the Swagger's "Authorize" popup, according to the form "Bearer `<token>`"
