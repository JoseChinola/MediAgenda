using AutoMapper;
using MediAgenda.DTOs.Permission;
using MediAgenda.Entities;

namespace MediAgenda.Mappings
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile() 
        {
            CreateMap<Permission, PermissionDto>();
            CreateMap<CreatePermissionDto, Permission>();
        }
    }
}
