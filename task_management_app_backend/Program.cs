using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using task_management_app_backend.api.Middleware;
using task_management_app_backend.data.Data;
using task_management_app_backend.data.IRepository;
using task_management_app_backend.data.Repository;
using task_management_app_backend.resources.CQRS.Handlers;
using task_management_app_backend.resources.Dtos.Validators;
using task_management_app_backend.resources.Mapper;

using task_management_app_backend.services.IServices;
using task_management_app_backend.services.Services;
//using static System.Collections.Immutable.ImmutableArray<T>;

var builder = WebApplication.CreateBuilder(args);





// JWT config
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("Jwt");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});

builder.Services.AddAuthorization();


// ----------------------------------
// Add services to the container
// ----------------------------------


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // <-- Angular dev server
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

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
builder.Services.AddScoped<IUserRepository, AuthenticationUserRepository>();

// Register Services
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

//chache services
builder.Services.AddMemoryCache();
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
    // 👇 Get API version descriptions
    var provider = builder.Services.BuildServiceProvider()
                                   .GetRequiredService<IApiVersionDescriptionProvider>();

    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerDoc(description.GroupName, new OpenApiInfo
        {
            Title = $"Task Management API {description.ApiVersion}",
            Version = description.ApiVersion.ToString()
        });
    }

    // 👇 ADD JWT SECURITY SCHEME
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token like this: **Bearer your_token_here**"
    });

    // 👇 REQUIRE JWT TOKEN FOR SECURED ENDPOINTS
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
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
app.UseCors("AllowAngularClient");  

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
