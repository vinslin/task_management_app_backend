using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using task_management_app_backend.api.Middleware;
using task_management_app_backend.data.Data;
using task_management_app_backend.data.IRepository;
using task_management_app_backend.data.Repository;
using task_management_app_backend.resources.CQRS.Handlers;
using task_management_app_backend.resources.Dtos.Validators;
using task_management_app_backend.resources.Mapper;
using task_management_app_backend.services.CQRS.Handlers;
using task_management_app_backend.services.IServices;
using task_management_app_backend.services.Services;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------
// Add services to the container
// ----------------------------------

builder.Services.AddControllers()
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblyContaining<CreateEmployeeDtoValidator>();
    });

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Register EF DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskRelatedProjectRepository, TaskRelatedProjectRepository>();
builder.Services.AddScoped<IUserRelatedTaskRepository, UserRelatedTaskRepository>();

// Register Services
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskService, TaskService>();

// ----------------------------------
// API Versioning
// ----------------------------------

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // "v1", "v2"
    options.SubstituteApiVersionInUrl = true;
});

// ----------------------------------
// Swagger Configuration (with API versioning)
// ----------------------------------
builder.Services.AddSwaggerGen(options =>
{
    var provider = builder.Services.BuildServiceProvider()
                                   .GetRequiredService<IApiVersionDescriptionProvider>();

    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerDoc(description.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = $"Task Management API {description.ApiVersion}",
            Version = description.ApiVersion.ToString()
        });
    }
});

builder.Services.AddMediatR(typeof(AddEmployeeHandler).Assembly);



var app = builder.Build();

// Get version provider for Swagger
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Swagger UI Setup
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                $"Task Management API {description.GroupName.ToUpperInvariant()}");
        }
    });
}

// ----------------------------------
// Middlewares & Endpoint Mapping
// ----------------------------------

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
