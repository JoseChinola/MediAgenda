using MediAgenda.Data;
using MediAgenda.Entities;
using MediAgenda.Interface.IAppointment;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MediAgenda.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
        }

        public void Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
        }

        public void Delete(Appointment appointment)
        {
            _context.Appointments.Remove(appointment);
        }

        public async Task<Appointment?> GetByIdAsync(Guid id)
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .ThenInclude(d => d.User)
                .Include(a => a.Patient)
                .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .ThenInclude(d => d.User)
                .Include(a => a.Patient)
                .ThenInclude(p => p.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(Guid doctorId)
        {
            return await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Include(a => a.Doctor)
                .ThenInclude(d => d.User)
                .Include(a => a.Patient)
                .ThenInclude(p => p.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId)
        {
            return await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .Include(a => a.Doctor)
                .ThenInclude(d => d.User)
                .Include(a => a.Patient)
                .ThenInclude(p => p.User)
                .ToListAsync();
        }

        public async Task<bool> ExistsAppointmentAtSameTime(Guid doctorId,DateTime appointmentDate)
        {
            return await _context.Appointments
                .AnyAsync(a => a.DoctorId == doctorId && a.AppointmentDate == appointmentDate);
        }

        public async Task<bool> PatientHasAppointmentAtSameTime(Guid patientId, DateTime appointmentDate)
        {
            return await _context.Appointments
                .AnyAsync(a => a.PatientId == patientId && a.AppointmentDate == appointmentDate);
        }

        public async Task<bool> ExistsAsync(Expression<Func<Appointment, bool>> predicate)
        {
            return await _context.Appointments.AnyAsync(predicate);
        }


        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
