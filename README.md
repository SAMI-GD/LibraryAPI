# ReadMe

## To prepare and run the `LibraryAPI solution`, follow these steps:

1. Make sure you have the .NET 7 SDK installed on your machine. You can download it from the official Microsoft website: **[https://dotnet.microsoft.com/download/dotnet/7.0](https://dotnet.microsoft.com/download/dotnet/7.0)**
2. Open the **`appsettings.json`** file in the **`LibraryAPI**` project and update the **`"DefaultConnection"**` key with your **`SQL database connection string`**.
3. Open the **`terminal`** (Command Prompt, PowerShell, or your preferred CLI) and navigate to the root folder of the **`LibraryAPI`** project.
4. Run the following command to install the required **NuGet** packages:
    
    ```bash
    dotnet restor
    ```
    
5. Run the following command to apply the database migrations and create the necessary tables in your SQL database:
    
    ```bash
    dotnet ef database update
    ```
    
6. Run the following command to start the `LibraryAPI` project:
    
    ```bash
    dotnet run
    ```
    
7. Open your web browser or use a tool like Postman to access the API endpoints. The default URL for the API will be **`http://localhost:5000`** or **`https://localhost:5001`**, depending on your project settings.
8. (Optional) If you have added `Swashbuckle.AspNetCore` and related packages, you can access the Swagger UI to interact with the API at **`http://localhost:5000/swagger`** or **`https://localhost:5001/swagger`**.

## To run the unit tests:

1. Make sure you have the required **NuGet packages installed** in your test project. You can check and install them through the terminal or Visual Studio.
2. Open the `terminal` and navigate to the root folder of the unit test project.
3. Run the following command to execute the unit tests:
    
    ```bash
    dotnet test
    ```
    
4. The test results will be displayed in the terminal.
