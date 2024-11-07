using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManager.AutoMappers;
using TaskManager.DTOs.Projects;
using TaskManager.DTOs.Tasks;
using TaskManager.Models;
using TaskManager.Models.Core;
using TaskManager.Repositories.Core.Interfaces;
using TaskManager.Repositories.Projects;
using TaskManager.Repositories.Tasks;
using TaskManager.Repositories.Users;
using TaskManager.Services.Projects;
using TaskManager.Services.Projects.Interfaces;
using TaskManager.Services.Tasks;
using TaskManager.Services.Tasks.Interfaces;
using TaskManager.Services.Tokens;
using TaskManager.Services.Tokens.Interface;
using TaskManager.Services.Users;
using TaskManager.Services.Users.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProjectService<ProjectDto, ProjectInsertDto, ProjectUpdateDto>, ProjectService>();
builder.Services.AddScoped<IProjectTaskService<ProjectTaskDto, ProjectTaskInsertDto, ProjectTaskUpdateDto>, ProjectTaskService>();


// Entity Framework
builder.Services.AddDbContext<ProjectManagerContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});

builder.Services.AddScoped<IGenericRepository<User>, UserRepository>();
builder.Services.AddScoped<IGenericRepository<Project>, ProjectRepository>();
builder.Services.AddScoped<IGenericRepository<ProjectTask>, ProjectTaskRepository>();

// Mappers
builder.Services.AddAutoMapper(typeof(MappingConfiguration));

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecretKey"]!))
        };
    });

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
