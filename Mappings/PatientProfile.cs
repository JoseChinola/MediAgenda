using AutoMapper;
using MediAgenda.DTOs.Patient;
using MediAgenda.Entities;

namespace MediAgenda.Mappings
{
    public class PatientProfile : Profile
    {
        public PatientProfile() 
        {
            CreateMap<Patient, PatientDto>()
                    .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));
         

            CreateMap<CreatePatientDto, Patient>();
        }
    }
}
