# SecureNotesAPI

## Overview
SecureNotesAPI is a RESTful Web API developed using ASP.NET Core Web API. The application allows users to register, log in using JWT authentication, and manage personal notes securely.

## Features
- User Registration
- User Login
- JWT Authentication
- Password Hashing using BCrypt
- Create Note
- View Notes
- Update Note
- Delete Note
- Swagger API Documentation

## Technologies Used
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server LocalDB
- JWT Authentication
- BCrypt.Net
- Swagger

## API Endpoints

### Authentication
- POST /api/auth/register
- POST /api/auth/login

### Notes
- GET /api/notes
- POST /api/notes
- PUT /api/notes/{id}
- DELETE /api/notes/{id}

## Author
Giridhar Gopal
