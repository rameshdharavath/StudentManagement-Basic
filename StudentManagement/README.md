# Student Management - Basic Full Stack

Stack:
- Frontend: React + TypeScript + Vite
- Backend: ASP.NET Core Web API (.NET 10)
- Database: SQL Server
- ORM: Entity Framework Core

## Prerequisites
- .NET 10 SDK
- Node.js 20+
- SQL Server / SQL Server Express
- npm

## 1. Database
Run `database/database.sql` in SQL Server Management Studio.

If using Windows Integrated Authentication, the default connection string is already configured for:
`Server=localhost;Database=StudentManagementDb;Trusted_Connection=True;TrustServerCertificate=True`

For SQL Server Express, update `backend/StudentManagement.Api/appsettings.json` to:
`Server=.\\SQLEXPRESS;Database=StudentManagementDb;Trusted_Connection=True;TrustServerCertificate=True`

## 2. Backend
Open a terminal:

    cd backend/StudentManagement.Api
    dotnet restore
    dotnet run

The API URL will be displayed in the terminal. Update the React API URL in:
`frontend/student-ui/src/services/studentService.ts`

Example:
`https://localhost:7001/api`


## 3. Frontend
Open another terminal:

    cd frontend/student-ui
    npm install
    npm run dev

Open the URL shown by Vite, normally:
`http://localhost:5173`

## API endpoints
GET    /api/students?search=&pageNumber=1&pageSize=10
GET    /api/students/{id}
POST   /api/students
PUT    /api/students/{id}
DELETE /api/students/{id}
