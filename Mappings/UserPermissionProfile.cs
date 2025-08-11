using AutoMapper;
using MediAgenda.DTOs.UserPermission;
using MediAgenda.Entities;

namespace MediAgenda.Mappings
{
    public class UserPermissionProfile : Profile
    {
        public UserPermissionProfile()
        {
            CreateMap<UserPermissionDto, UserPermission>();
            CreateMap<UserPermission, UserPermissionResponseDto>()
                .ForMember(dest => dest.Fullname,
                opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(dest => dest.PermissionName,
                opt => opt.MapFrom(src => src.Permission.Name));
        }
    }
}
