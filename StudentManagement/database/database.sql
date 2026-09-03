IF DB_ID('StudentManagementDb') IS NULL
BEGIN
    CREATE DATABASE StudentManagementDb;
END
GO

USE StudentManagementDb;
GO

IF OBJECT_ID('dbo.Students', 'U') IS NULL
BEGIN
    CREATE TABLE Students
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        FirstName NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(100) NOT NULL,
        Email NVARCHAR(200) NOT NULL,
        Phone NVARCHAR(20) NULL,
        DateOfBirth DATE NULL,
        Gender NVARCHAR(20) NULL,
        Course NVARCHAR(100) NULL,
        Department NVARCHAR(100) NULL,
        Address NVARCHAR(500) NULL,
        CreatedDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
        ModifiedDate DATETIME2 NULL
    );

    CREATE UNIQUE INDEX IX_Students_Email ON Students(Email);
END
GO

IF NOT EXISTS (SELECT 1 FROM Students)
BEGIN
    INSERT INTO Students
    (FirstName, LastName, Email, Phone, DateOfBirth, Gender, Course, Department, Address)
    VALUES
    ('John', 'Smith', 'john@gmail.com', '9876543210', '2000-05-10', 'Male',
     'Computer Science', 'Engineering', 'Hyderabad'),
    ('Mary', 'Joseph', 'mary@gmail.com', '9876543211', '2001-08-20', 'Female',
     'Electronics', 'Engineering', 'Warangal'),
    ('David', 'Kumar', 'david@gmail.com', '9876543212', '1999-12-15', 'Male',
     'Mechanical', 'Engineering', 'Hyderabad');
END
GO
