using AutoMapper;
using MediAgenda.DTOs.Appointment;
using MediAgenda.Entities;

namespace MediAgenda.Mappings
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentDto>();
            CreateMap<CreateAppointmentDto, Appointment>()
                .ForMember(dest => dest.Id,
                otp => otp.MapFrom(_ => Guid.NewGuid()));
        }
    }
}
