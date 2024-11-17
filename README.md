---

# Cafe Management App (Backend) - .NET Core

This repository contains the backend for the **Cafe Management App**, built using **.NET Core**.

## Table of Contents

1. [About](#about)
2. [Prerequisites](#prerequisites)
3. [Dependencies](#dependencies)
4. [Getting Started](#getting-started)
5. [API Endpoints](#api-endpoints)
6. [Further Improvements](#further-improvements)

## About

The **Cafe Management App Backend** is built with **.NET Core** and provides APIs for managing cafes and employees. The backend handles business logic, data persistence, and API communication with the frontend, which is built using **React**.

## Prerequisites

Before running this application, ensure you have the following tools installed:

- **.NET Core SDK** (8.0 or higher)
- **MySQL Server** (or your preferred relational database)
- **Postman** (optional, for API testing)
- **Visual Studio** or **Visual Studio Code** (optional, for editing and debugging)

## Dependencies

- `Microsoft.EntityFrameworkCore`: ^8.0.2
- `Microsoft.EntityFrameworkCore.Tools`: ^8.0.2
- `Pomelo.EntityFrameworkCore.MySql`: ^8.0.2
- `Swashbuckle.AspNetCore`: ^7.0 (for Swagger UI integration)

## Getting Started

### 1. Clone the Repository

Clone the repository to your local machine:

```bash
git clone https://github.com/praveenwanninayake/CafeService_BE.git
cd cafe-management-app-backend
```

### 2. Install Dependencies

Ensure the **.NET Core SDK** is installed, then restore the necessary dependencies:

```bash
dotnet restore
```

### 3. Configure the Database

Ensure your database server (e.g., MySQL) is running and accessible. Update the connection string in the `appsettings.json` file to match your database configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=cafeprod;User Id=your_user;Password=your_password;"
  }
}
```

Replace `your_user` and `your_password` with your actual database credentials.

### 4. Run Migrations

If you're using **Entity Framework Core** for database management, apply any pending migrations:

```bash
dotnet ef database update
```

### 5. Run the Application

Once everything is configured, run the backend locally:

```bash
dotnet run
```

The app will be hosted at `http://localhost:5052` by default.

### 6. Testing the APIs

You can use **Postman** or any API testing tool to interact with the backend. For example, to fetch all employees, send a `GET` request to:

```
GET http://localhost:5052/api/employees?page=1&items_per_page=10
```

### 7. Swagger UI

Swagger is enabled for API documentation and interactive testing. Open your browser and visit:

```
http://localhost:5052/swagger/index.html
```

## API Endpoints

Below are the main API endpoints for managing cafes and employees.

### **Cafe Controller**

- **GetAllCafe**  
  `GET http://localhost:5052/api/cafes?page=1&items_per_page=10&location=`

- **GetCafeById**  
  `GET http://localhost:5052/api/cafes/{id}`

- **InsertCafe**  
  `POST http://localhost:5052/api/cafe`

- **UpdateCafe**  
  `PUT http://localhost:5052/api/cafe`

- **DeleteCafe**  
  `DELETE http://localhost:5052/api/cafe`

- **GetCafeList**  
  `GET http://localhost:5052/api/cafe-list`

### **Employee Controller**

- **GetAllEmployee**  
  `GET http://localhost:5052/api/employees?page=1&items_per_page=10`

- **GetEmployeeById**  
  `GET http://localhost:5052/api/employees/{id}`

- **InsertEmployee**  
  `POST http://localhost:5052/api/employee`

- **UpdateEmployee**  
  `PUT http://localhost:5052/api/employee`

- **DeleteEmployee**  
  `DELETE http://localhost:5052/api/employee`

## Further Improvements

- **Security**: Implement JWT authentication and authorization for secure API access.
- **Order Management**: Add endpoints for managing orders, customers, and inventory.
- **Real-Time Features**: Implement background services for real-time notifications (e.g., order updates).
- **Performance Enhancements**: Introduce caching, rate-limiting, and logging for better scalability.
- **Testing**: Develop unit and integration tests to ensure code reliability and maintainability.

---
