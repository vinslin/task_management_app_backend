using MediatR;
using task_management_app_backend.resources.Dtos.ResponseDto;

namespace task_management_app_backend.services.CQRS.Queries
{
    public class GetParticularEmployeeTasksQuery : IRequest<List<GetTaskByEmployeeIdDto>>
    {
        public Guid EmployeeId { get; set; }

        public GetParticularEmployeeTasksQuery(Guid employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
