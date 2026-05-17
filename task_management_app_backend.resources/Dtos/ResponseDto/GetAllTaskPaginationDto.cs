using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_management_app_backend.resources.Dtos.ResponseDto
{
    public class GetAllTaskPaginationDto
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
        public List<ResponseCreateTaskDto> Tasks { get; set; } = new List<ResponseCreateTaskDto>();
    }
}
