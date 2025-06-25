
namespace task_management_app_backend.resources.Dtos.ResponseDto
{
    public class ResponseEmployeeDto

    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        
        public string Email { get; set; }   

        public string Role { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }


        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }



    }
}
