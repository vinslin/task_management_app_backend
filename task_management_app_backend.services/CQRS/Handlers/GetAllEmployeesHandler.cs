using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using task_management_app_backend.data.Data;
using task_management_app_backend.services.CQRS.Queries;
using task_management_app_backend.resources.Dtos.ResponseDto;
{
    
}

namespace task_management_app_backend.services.CQRS.Handlers
{
    public class GetAllEmployeesHandler  : IRequestHandler <GetAllEmployeesQuery, List<ResponseEmployeeDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllEmployeesHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ResponseEmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken) {

            var employees = await _context.Set<ResponseEmployeeDto>()
                .FromSqlRaw("EXEC GetAllEmployees")
                .ToListAsync();

            return employees;

            
        }
    }
}
