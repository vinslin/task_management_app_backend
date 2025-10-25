using Azure.Core;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using task_management_app_backend.data.Data;
using task_management_app_backend.resources.Dtos.ResponseDto;
using task_management_app_backend.services.CQRS.Queries;

namespace task_management_app_backend.services.CQRS.Handlers
{
    public class GetTasksByEmployeeIdQueryHandler : IRequestHandler<GetParticularEmployeeTasksQuery, List<GetTaskByEmployeeIdDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetTasksByEmployeeIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetTaskByEmployeeIdDto>> Handle(GetParticularEmployeeTasksQuery req, CancellationToken cancellation) {


            var parameter = new SqlParameter("@EmployeeId",req.EmployeeId);

         //   var parameter = new SqlParameter("@EmployeeId", request.EmployeeId);

            var tasks = await _context.Database
                .SqlQueryRaw<GetTaskByEmployeeIdDto>(
                    $"EXEC GetTasksByEmployeeId @EmployeeId",
                    parameter)
                .ToListAsync(cancellation);




            return tasks;

        }

    }
}
