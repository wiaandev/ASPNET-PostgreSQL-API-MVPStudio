
# MVP Studio - ASP.NET API

## Introduction

MVP Studio's ASP.NET API is the backend component of the Creative Agency Management application. It provides the necessary endpoints to interact with the application's data, including clients, projects, staff, and financial information. This guide will walk you through the process of setting up and launching the API.

## Prerequisites

Before launching the MVP Studio ASP.NET API, ensure you have the following prerequisites in place:

- **Visual Studio:** Make sure you have Visual Studio installed on your development machine.

- **.NET SDK:** Ensure you have the [.NET SDK](https://dotnet.microsoft.com/download/dotnet/5.0) installed, preferably .NET 5.0 or later.

- **PostgreSQL Database:** The API relies on a PostgreSQL database to store and manage data. You can set up your PostgreSQL database locally or use a cloud-based service like [Aiven](https://aiven.io/).

## Setup

Follow these steps to set up and launch the MVP Studio ASP.NET API:

### 1. Clone the Repository

```bash
git clone https://github.com/wiaandev/ASPNET-PostgreSQL-API-MVPStudio.git
```

### 2. Configure the Database Connection

Open the `appsettings.json` file in the project root and configure the database connection string. Replace the placeholders with your PostgreSQL database connection details:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=mydbhost;Port=mydbport;Database=mydbname;Username=mydbuser;Password=mydbpassword;"
},
```

### 3. Migrate the Database

Run the following command in the project directory to apply the database migrations:

```bash
dotnet ef database update
```

### 4. Build and Run the API

Use the following commands to build and run the API:

```bash
dotnet build
dotnet run
```

The API will start and be accessible at `http://localhost:5000` (HTTP) or `https://localhost:5001` (HTTPS).

### 5. Test the API

You can use tools like [Postman](https://www.postman.com/) or [Swagger](https://swagger.io/tools/swagger-ui/) to test the API endpoints. We prefer Swagger, it provides an interactive API documentation interface that allows you to explore and test the available endpoints.

Open your web browser and navigate to `http://localhost:5000/swagger` (HTTP) or `https://localhost:5001/swagger` (HTTPS) to access the Swagger UI.

## Usage

The MVP Studio ASP.NET API provides a RESTful interface for managing various aspects of the creative agency application. You can use the API to perform actions such as:

- **Client Management:** Create, read, update, and delete client records.

- **Project Tracking:** Manage project details, including creation, updates, and tracking progress.

- **Staff Management:** Maintain information about staff members, their roles, and assignments.

- **Financial Insights:** Record financial transactions, income, and expenses.

## Contributing

Contributions to the MVP Studio ASP.NET API are welcome! If you have any improvements, bug fixes, or feature suggestions, please open an issue or submit a pull request.

## License

The MVP Studio ASP.NET API is licensed under the [MIT License](LICENSE).


## Acknowledgments

- Special thanks to the open-source community and the creators of .NET Core for making this project possible, Wiaan Duvenhage, Shanré Scheepers & Liam Wedge.

