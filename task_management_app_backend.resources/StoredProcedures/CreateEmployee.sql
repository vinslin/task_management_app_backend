USE [TaskManagementAppDb]
GO
/****** Object:  StoredProcedure [dbo].[AddEmployee]    Script Date: 25-06-2025 18:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AddEmployee]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @Role NVARCHAR(50),
    @CreatedAt DATETIME,
    @UpdatedBy NVARCHAR(100),
    @CreatedBy NVARCHAR(100)
AS
BEGIN
    INSERT INTO Employees (ID, Name, Email, Role, CreatedAt, UpdatedAt, UpdatedBy, CreatedBy)
    VALUES (@Id, @Name, @Email, @Role, @CreatedAt, @CreatedAt, @UpdatedBy, @CreatedBy)
END
