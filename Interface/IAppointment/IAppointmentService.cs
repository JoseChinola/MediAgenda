using MediAgenda.DTOs.Appointment;
using MediAgenda.Entities;

namespace MediAgenda.Interface.IAppointment
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAllAsync();
        Task<AppointmentDto?> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(CreateAppointmentDto createAppointmentDto);
        Task<bool> UpdateAppointmentStatusAsync(UpdateAppointmentStatusDto dto);
        Task<bool> RescheduleAppointmentAsync(UpdateAppointmentDto dto);

        Task<IEnumerable<AppointmentDto>> GetByDoctorAsync(Guid doctorId);
        Task<IEnumerable<AppointmentDto>> GetByPatientAsync(Guid patientId);
    }
}
