using AutoMapper;
using MediAgenda.DTOs.Reception;
using MediAgenda.Entities;

namespace MediAgenda.Mappings
{
    public class ReceptionProfile : Profile
    {
        public ReceptionProfile()
        {
            CreateMap<User, ReceptionDto>()
                .ForMember(dest => dest.RoleName,
                opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

        }
    }
}
