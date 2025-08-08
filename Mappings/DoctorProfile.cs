using AutoMapper;
using MediAgenda.DTOs.Doctor;
using MediAgenda.Entities;

namespace MediAgenda.Mappings
{
    public class DoctorProfile: Profile
    {
        public DoctorProfile()
        {
            CreateMap<CreateDoctorDto, Doctor>();
            CreateMap<Doctor, DoctorDto>()
              .ForMember(dest => dest.FullName,
               opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));
             
            
             
        }
    }
}
