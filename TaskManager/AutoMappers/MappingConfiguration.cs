using AutoMapper;
using TaskManager.DTOs.Users;
using TaskManager.Models;

namespace TaskManager.AutoMappers
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<UserInsertDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
