CREATE PROCEDURE GetAllEmployees
AS
BEGIN
    SELECT ID, Name, Email, Role, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy
    FROM Employees
END
