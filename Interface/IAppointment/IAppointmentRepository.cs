using MediAgenda.Entities;
using System.Linq.Expressions;

namespace MediAgenda.Interface.IAppointment
{
    public interface IAppointmentRepository
    {
        Task AddAsync(Appointment appointment);
        void Update(Appointment appointment);
        void Delete(Appointment appointment);
        Task<Appointment?> GetByIdAsync(Guid id);
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<IEnumerable<Appointment>> GetByDoctorIdAsync(Guid doctorId);
        Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId);
        Task<bool> ExistsAppointmentAtSameTime(Guid doctorId, DateTime appointmentDateTime);
        Task<bool> PatientHasAppointmentAtSameTime(Guid patientId, DateTime appointmentDateTime);
        Task<bool> ExistsAsync(Expression<Func<Appointment, bool>> predicate);
        Task<List<DateTime>> GetAppointmentsByDoctorAndDateAsync(Guid doctorId, DateTime date);
        Task<bool> SaveChangesAsync();
    }
}
