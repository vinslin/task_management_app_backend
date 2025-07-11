using MediatR;

namespace task_management_app_backend.resources.CQRS.Commands
{
    public class AddEmployeeCommand : IRequest<Guid>  //last ullathu guid la return pannum lik oru function mathiri
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public string? CreatedBy { get; set; } = "System";

        public string? UpdatedBy { get; set; } = null;
    }
}
