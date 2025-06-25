using AutoMapper;
using task_management_app_backend.data.Entities;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.resources.Dtos.ResponseDto;

namespace task_management_app_backend.resources.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<CreateEmployeeDto, Employee>()
    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName));

            CreateMap<task_management_app_backend.data.Entities.Task, ResponseCreateTaskDto>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.ID));
            // Project mapping
            CreateMap<CreateProjectDto, Project>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProjectName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            // Employee mapping
            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

            // Task mapping for nested task lists (Employee.UserTasks → ResponseCreateTaskDto)
            CreateMap<UserReleatedTask, ResponseCreateTaskDto>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Task.ID))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Task.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Task.Description))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Task.Priority))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.Task.DueDate))
                .ForMember(dest => dest.EmployeeId, opt => opt.Ignore()); // You’re setting this manually
        }
    }
}
