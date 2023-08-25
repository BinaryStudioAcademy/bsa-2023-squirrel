-- Create database
CREATE DATABASE TestDatabase;
GO

USE TestDatabase;

-- Create tables
CREATE TABLE OneTable (
    ID INT PRIMARY KEY,
    Name NVARCHAR(50)
);

CREATE TABLE AnotherTable (
    ID INT PRIMARY KEY,
    Description NVARCHAR(100)
);

-- Insert sample data
INSERT INTO OneTable (ID, Name) VALUES (1, 'Item 1');
INSERT INTO AnotherTable (ID, Description) VALUES (1, 'Description 1');
GO

-- Create stored procedure
CREATE PROCEDURE TestStoredProcedure
AS
BEGIN
    -- Your stored procedure logic here
    SELECT 'Hello from Stored Procedure' AS Message;
END;
GO

-- Create view
CREATE VIEW TestView AS
SELECT t1.ID, t1.Name, t2.Description
FROM OneTable t1
JOIN AnotherTable t2 ON t1.ID = t2.ID;
GO

-- Create function
CREATE FUNCTION TestFunction()
RETURNS NVARCHAR(50)
AS
BEGIN
    DECLARE @Result NVARCHAR(50);
    SET @Result = 'Hello from Function';
    RETURN @Result;
END;