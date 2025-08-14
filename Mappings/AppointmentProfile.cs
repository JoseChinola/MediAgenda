using AutoMapper;
using MediAgenda.DTOs.Appointment;
using MediAgenda.Entities;

namespace MediAgenda.Mappings
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => $"{src.Doctor.User.FirstName} {src.Doctor.User.LastName}" ))
            .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => $"{src.Patient.User.FirstName} {src.Patient.User.LastName}"));
            ;
            CreateMap<CreateAppointmentDto, Appointment>()
                .ForMember(dest => dest.Id,
                otp => otp.MapFrom(_ => Guid.NewGuid()));
               
        }
    }
}
