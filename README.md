# ReadMe

## To prepare and run the `LibraryAPI solution`, follow these steps:

1. Install the required software and tools:
    - Visual Studio 2022 or later
    - .NET 7 SDK
    - SQL Server (or another database system of your choice)
    - SQL Server Management Studio (or another database management tool of your choice)
2. 1. Clone the `LibraryAPI` project from the [repository](https://github.com/SAMI-GD/LibraryAPI) or extract it from the provided zip file.
3. Open the `LibraryAPI` solution in **Visual Studio**.
4. Update the **`appsettings.json`** file with your database connection string:
    
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=YOUR_DATABASE_NAME;User Id=YOUR_USER_ID;Password=YOUR_PASSWORD;"
    }
    ```
    
5. Open the **Package Manager Console in Visual Studio** `(Tools > NuGet Package Manager > Package Manager Console)`.
6. Run the following commands to apply the migrations and seed the database:
    
    ```sql
    add-migration {migration name}
    update-database
    ```
    
7. Ensure that all required NuGet packages are installed in your API and Unit Test projects. If any packages are missing, install them using the NuGet Package Manager.
    - `Nuget` packages used in API :
        - `Entity Framework Core`
        - `Entity Framework Core Tools`
        - `Entity Framework Core SQL`
        - `Entity Framework Core Design`
        - `Auto Mapper`
        - `Auto Mapper Extensions Dependency Injection`
        - `Swashbuckle AspNetCore`
        - `Swashbuckle AspNetCore.Annotations`
    - `Nuget` packages used in Unit Test :
        - `xunit`
        - `FakeItEasy`
        - `FluentAssertions`
8. Build the solution `(Build > Build Solution)` to ensure there are no errors.
9. To run the API, set the `LibraryAPI` project as the startup project and press `F5` or click `"Run"` in **Visual Studio**. The API will be launched in your default browser, and Swagger documentation should be displayed.
10. To run the unit tests, open the `Test Explorer` in **Visual Studio** `(Test > Test Explorer)` and click `"Run All Tests."` The test results will be displayed in the Test Explorer window.

Once the API is running, you can use tools like Postman or the built-in Swagger UI to test the API endpoints.
