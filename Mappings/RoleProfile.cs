using AutoMapper;
using MediAgenda.DTOs.Roles;
using MediAgenda.Entities;

namespace MediAgenda.Mappings
{
    public class RoleProfile:Profile
    {
        public RoleProfile() 
        {
            // DTO a Entidad
            CreateMap<CreateRoleDto, Role>();
            // Entidad a DTO
            CreateMap<Role, RoleDto>();

        }
    }
}
