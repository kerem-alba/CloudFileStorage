# CloudFileStorage

CloudFileStorage is a modular file storage application built with ASP.NET Core.  
It allows users to upload, download, and share files with various access permissions.  
The project is structured as multiple microservices and follows clean architectural principles.

## Features

- File upload and download
- JWT-based authentication and authorization
- File sharing with user-specific permissions
- Public, private, and restricted access options
- Modular service architecture with clear separation of concerns
- CQRS pattern implemented using SMediator

## Project Structure

The project is divided into several modules, each responsible for a specific functionality.

### Modules

- CloudFileStorage.AuthApi/  
  Handles user registration, login, and JWT token management (localhost:5001)

- CloudFileStorage.Common/  
  Contains shared constants, helpers, and DTOs

- CloudFileStorage.FileMetadataApi/  
  Manages file metadata and sharing settings (localhost:5002)

- CloudFileStorage.FileStorageApi/  
  Handles physical file upload/download operations (localhost:5003)

- CloudFileStorage.GatewayApi/  
  Acts as API Gateway and routes requests to other services (localhost:5000)

- CloudFileStorage.UI/  
  ASP.NET Core MVC frontend project for user interaction (localhost:5500)

## Getting Started

To run the project locally, follow these steps:

1. Clone the repository.
2. Configure the connection strings in the relevant `appsettings.json` files.
3. Apply database migrations if needed.
4. Run each project individually or configure them to launch together.
5. Access the UI project to interact with the application.

## API Endpoints

Base URL: https://localhost:5000/api

### Auth

- POST `/Auth/login`
- POST `/Auth/register`
- POST `/Auth/refresh`
- GET `/User/{id}`
- GET `/User/list`
- POST `/User/names`

### File Metadata

- GET `/Files`
- GET `/Files/{id}`
- GET `/Files/{id}/accessible`
- POST `/Files`
- PUT `/Files/{id}`
- DELETE `/Files/{id}`

### File Shares

- GET `/FileShares/shared-with-me`
- POST `/FileShares`
- POST `/FileShares/check-access`
- GET `/FileShares/by-file/{id}`

### File Storage

- POST `/FileStorage/upload`
- GET `/FileStorage/download?fileId={id}&fileName={name}`

## Tech Stack

- ASP.NET Core 8
- Entity Framework Core 9
- PostgreSQL
- SMediator (CQRS pattern)
- FluentValidation
- JWT Authentication
- Bootstrap 5
- Swagger for API documentation

## Author

This project is developed and maintained by Kerem Albayrak.
