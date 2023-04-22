# ReadMe

## To prepare and run the `LibraryAPI solution`, follow these steps:

1. Install the required software and tools:
    - Visual Studio 2022 or later
    - .NET 7 SDK
    - SQL Server (or another database system of your choice)
    - SQL Server Management Studio (or another database management tool of your choice)
2. Clone the `LibraryAPI` project from the [repository](https://github.com/SAMI-GD/LibraryAPI) or extract it from the provided zip file.
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

## **API Definition**

> Library API `v1`
> 

### ****Users****

### Get ****Users****

- Get All ****Users****
    
    ```jsx
    GET /api/Users
    ```
    
    ```html
    CODE : 200
    ```
    
    ```json
    [
      {
        "userID": 0,
        "firstName": "string",
        "lastName": "string",
        "email": "string",
        "phoneNumber": "string"
      }
    ]
    ```
    
- Get ****User by ID****
    
    ```jsx
    GET /api/Users/{id}
    ```
    
    ```html
    CODE : 200
    ```
    
    ```json
      {
        "userID": 0,
        "firstName": "string",
        "lastName": "string",
        "email": "string",
        "phoneNumber": "string"
      }
    ```
    

### Create a ****User****

```jsx
POST /api/Users
```

```html
CODE : 201
```

```json
{
  "userID": 17,
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "phoneNumber": "string",
  "borrowTransactions": null
}
```

### Update a ****User****

```jsx
PUT /api/Users/{id}
```

```html
CODE : 200
```

```json
{
  "user": {
    "userID": 1,
    "firstName": "Jhon",
    "lastName": "Smith",
    "email": "jhon@email.com",
    "phoneNumber": "112-123-1234"
  },
  "message": "Library item updated successfully."
}
```

### Delete a ****User****

```jsx
Delete /api/Users/{id}
```

```html
CODE : 200
```

```json
{
  "message": "User deleted successfully."
}
```

### ****Library Items****

### Get ****Library Items****

- Get All ****Library Items****
    
    ```jsx
    GET api/LibraryItems
    ```
    
    ```html
    CODE : 200
    ```
    
    ```json
    [{
        "itemID": 1,
        "title": "Book1",
        "author": "Author1",
        "itemType": "Book",
        "publicationDate": "2000-01-01T00:00:00",
        "availabilityStatus": "Borrowed"
      }]
    ```
    
- Get ****Library Item by ID****
    
    ```jsx
    GET api/LibraryItems/{id}
    ```
    
    ```html
    CODE : 200
    ```
    
    ```json
    {
        "itemID": 1,
        "title": "Book1",
        "author": "Author1",
        "itemType": "Book",
        "publicationDate": "2000-01-01T00:00:00",
        "availabilityStatus": "Borrowed"
      }
    ```
    
- Get Library Items by Title
    
    ```jsx
    GET /api/LibraryItems/search?title={title}
    ```
    
    ```html
    CODE : 200
    ```
    
    ```json
    [
      {
        "title": "Book1",
        "author": "Author1",
        "itemType": "Book",
        "publicationDate": "2000-01-01T00:00:00",
        "availabilityStatus": "Borrowed"
      }
    ]
    ```
    
- Get Books by Title
    
    ```jsx
    GET api/LibraryItems/search/book?title={title}
    ```
    
    ```html
    CODE : 200
    ```
    
    ```json
    [
      {
        "title": "Book1",
        "author": "Author1",
        "itemType": "Book",
        "publicationDate": "2000-01-01T00:00:00",
        "availabilityStatus": "Borrowed"
      }
    ]
    ```
    
- Get Items by Author and Availability Status
    
    ```jsx
    GET /api/LibraryItems/search/author-availability?author={author}&availabilityStatus={availabilityStatus}
    ```
    
    ```html
    CODE : 200
    ```
    
    ```json
    [
      {
        "title": "Book1",
        "author": "Author1",
        "itemType": "Book",
        "publicationDate": "2000-01-01T00:00:00",
        "availabilityStatus": "Borrowed"
      }
    ]
    ```
    
- Get Books by Author and Availability Status
    
    ```jsx
    GET /api/LibraryItems/search/author-availability-books?author={author}&availabilityStatus={availabilityStatus}
    ```
    
    ```html
    CODE : 200
    ```
    
    ```json
    [
      {
        "title": "Book1",
        "author": "Author1",
        "itemType": "Book",
        "publicationDate": "2000-01-01T00:00:00",
        "availabilityStatus": "Borrowed"
      }
    ]
    ```
    

### Create a ****Library Item****

```jsx
POST api/LibraryItems
```

```html
CODE : 201
```

```json
{
  "itemID": 1,
  "title": "Book1",
  "author": "Author 1",
  "publicationDate": "2023-04-20T01:28:18.504Z",
  "itemType": "Book",
  "availabilityStatus": "Available",
  "borrowTransactions": null
}
```

### Update a ****Library Item****

```jsx
PUT api/LibraryItems/{id}
```

```html
CODE : 200
```

```json
{
  "libraryItem": {
    "itemID": 1,
    "title": "Book 1",
    "author": "Author 1",
    "itemType": "Book",
    "publicationDate": "2023-04-22T01:30:58.18Z",
    "availabilityStatus": "Available"
  },
  "message": "Library item updated successfully."
}
```

### Delete a ****Library Item****

```jsx
Delete api/LibraryItems/{id}
```

```html
CODE : 200
```

```json
{
  "message": "Library item deleted successfully."
}
```

### ****Borrow Transactions****

### Get ****Borrow Transaction****

- Get All ****Borrow Transactions****
    
    ```jsx
    GET api/BorrowTransactions
    ```
    
    ```html
    CODE : 200
    ```
    
    ```json
    [
    {
        "transactionID": 2,
        "userID": 2,
        "userName": "Jane",
        "itemID": 3,
        "libraryItemTitle": "Newspaper1",
        "borrowDate": "2023-01-01T00:00:00",
        "dueDate": "2023-01-15T00:00:00",
        "returnDate": null,
        "lateFee": 0
      }
    ]
    ```
    
- Get All ****Borrow Transactions****
    
    ```jsx
    GET api/BorrowTransactions/{id}
    ```
    
    ```html
    CODE : 200
    ```
    
    ```json
    {
        "transactionID": 2,
        "userID": 2,
        "userName": "Jane",
        "itemID": 3,
        "libraryItemTitle": "Newspaper1",
        "borrowDate": "2023-01-01T00:00:00",
        "dueDate": "2023-01-15T00:00:00",
        "returnDate": null,
        "lateFee": 0
      }
    ```
    
- Get The Borrowing History of a User
    
    ```jsx
    GET /api/BorrowTransactions/user/{userId}
    ```
    
    ```html
    CODE : 200
    ```
    
    ```json
    [
    {
        "transactionID": 2,
        "userID": 2,
        "userName": "Jane",
        "itemID": 3,
        "libraryItemTitle": "Newspaper1",
        "borrowDate": "2023-01-01T00:00:00",
        "dueDate": "2023-01-15T00:00:00",
        "returnDate": null,
        "lateFee": 0
      }
    ]
    ```
    

### Assign a Book/Item to a User

```jsx
POST api/BorrowTransactions
```

```html
CODE : 201
```

```json
{
  "transaction": {
    "transactionID": 5,
    "userID": 18,
    "userName": "User 18",
    "itemID": 7,
    "libraryItemTitle": "Book 7",
    "borrowDate": "2023-04-22T01:35:38.946Z",
    "dueDate": "2023-04-28T01:35:38.946Z",
    "returnDate": null,
    "lateFee": 0
  },
  "libraryItem": {
    "itemID": 7,
    "title": "Book 7",
    "author": "author 7",
    "itemType": "Book",
    "publicationDate": "2023-04-22T01:36:04.656",
    "availabilityStatus": "Borrowed"
  },
  "message": "The book has been assigned to the user successfully."
}
```
