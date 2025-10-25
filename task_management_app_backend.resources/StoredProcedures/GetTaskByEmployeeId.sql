CREATE PROCEDURE GetTasksByEmployeeId
    @EmployeeId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        t.ID, 
        t.Title, 
        t.Description, 
        t.IsCompleted, 
        t.Priority, 
        t.DueDate
    FROM userReleatedTasks AS u
    INNER JOIN Tasks t ON t.ID = u.TaskId
    WHERE u.EmployeeId = @EmployeeId;
END;
