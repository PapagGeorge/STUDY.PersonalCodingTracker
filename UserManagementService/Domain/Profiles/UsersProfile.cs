using AutoMapper;
using Domain.Models;
using Domain.Dtos;

namespace Domain.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            // Map for creating a new user
            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

            // Map for reading a user
            CreateMap<User, UserReadDto>();

            // Map for updating an existing user
            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()); // Ensure CreatedAt is not changed
        }
    }
}
