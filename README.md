# Inventory Management API

## Overview

The **Inventory Management API** is a sample project that demonstrates a basic implementation of an inventory management system using ASP.NET Core. The project is built with domain-driven design (DDD) principles, leveraging various best practices such as dependency injection, validation, caching, and unit testing.

## Features

- **CRUD operations for Products and Orders**
- **Validation** using FluentValidation
- **Caching** to improve performance
- **Unit Testing** with Moq and xUnit
- **Domain-Driven Design (DDD)** with well-defined domain models and repository patterns

## Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- FluentValidation
- Moq (for mocking in unit tests)
- xUnit (for unit testing)
- In-Memory Caching
- Dependency Injection

## Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- A SQL Server instance for the database (or you can configure a different database provider)

### Setup

1. **Clone the repository:**

   ```sh
   git clone https://github.com/yourusername/inventory-management-api.git
   cd inventory-management-api
