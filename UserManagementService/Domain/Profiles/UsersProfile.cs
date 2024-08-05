using AutoMapper;
using Domain.Models;
using Domain.Dtos;

namespace Domain.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
        }
    }
}
