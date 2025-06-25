using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using task_management_app_backend.data.Data;
using task_management_app_backend.resources.CQRS.Commands;

namespace task_management_app_backend.resources.CQRS.Handlers
{
    public class AddEmployeeHandler : IRequestHandler<AddEmployeeCommand, Guid>
    {
        private readonly ApplicationDbContext _context;

        public AddEmployeeHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            var now = DateTime.UtcNow;

            var sql = "EXEC AddEmployee @Id, @Name, @Email, @Role, @CreatedAt,@UpdatedBy, @CreatedBy";

            var parameters = new[]
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Name", request.UserName),
                new SqlParameter("@Email", request.Email),
                new SqlParameter("@Role", request.Role),
                new SqlParameter("@CreatedAt", now),
                new SqlParameter("@UpdatedBy", request.UpdatedBy ?? null),
                new SqlParameter("@CreatedBy", request.CreatedBy ?? "System")
            };

            await _context.Database.ExecuteSqlRawAsync(sql, parameters);

            return id;
        }
    }
}
