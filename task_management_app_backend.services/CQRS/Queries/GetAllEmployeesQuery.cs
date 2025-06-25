using MediatR;
using task_management_app_backend.resources.CQRS.Handlers;
using task_management_app_backend.resources.Dtos.ResponseDto;

namespace task_management_app_backend.services.CQRS.Queries
{
    public class GetAllEmployeesQuery : IRequest<List <ResponseEmployeeDto>>
    {
       
    }
}
