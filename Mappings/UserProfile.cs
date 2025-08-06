using AutoMapper;
using MediAgenda.DTOs.User;
using MediAgenda.Entities;

namespace MediAgenda.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // DTO to Entity
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // Entity to DTO
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
        }
    }
}
