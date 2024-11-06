using AutoMapper;
using TaskManager.DTOs.Projects;
using TaskManager.DTOs.Users;
using TaskManager.Models;

namespace TaskManager.AutoMappers
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            // Users
            CreateMap<UserInsertDto, User>();
            CreateMap<User, UserDto>();

            // Projects
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectInsertDto, Project>();
            CreateMap<ProjectUpdateDto, Project>();
        }
    }
}
