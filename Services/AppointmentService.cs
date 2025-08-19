using AutoMapper;
using MediAgenda.DTOs.Appointment;
using MediAgenda.Entities;
using MediAgenda.Interface.IAppointment;
using MediAgenda.Repositories;

namespace MediAgenda.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;
        private readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAsync()
        {
            var appointments = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<AppointmentDto?> GetByIdAsync(Guid id)
        {
            var appointment = await _repository.GetByIdAsync(id);
            return appointment == null ? null : _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task<IEnumerable<AppointmentDto>> GetByDoctorAsync(Guid doctorId)
        {
            var appointments = await _repository.GetByDoctorIdAsync(doctorId);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<IEnumerable<AppointmentDto>> GetByPatientAsync(Guid patientId)
        {
            var appointments = await _repository.GetByPatientIdAsync(patientId);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<bool> CreateAsync(CreateAppointmentDto dto)
        {
            if (await _repository.ExistsAppointmentAtSameTime(dto.DoctorId, dto.AppointmentDate)) return false;
            if (await _repository.PatientHasAppointmentAtSameTime(dto.PatientId, dto.AppointmentDate)) return false;

            var appointment = _mapper.Map<Appointment>(dto);

            appointment.AppointmentDate = DateTime.SpecifyKind(dto.AppointmentDate, DateTimeKind.Utc);

            await _repository.AddAsync(appointment);
            return await _repository.SaveChangesAsync();

        }

        public async Task<bool> UpdateAppointmentStatusAsync(UpdateAppointmentStatusDto dto)
        {
            var appointment = await _repository.GetByIdAsync(dto.AppointmentId);
            if (appointment == null) return false;

            if (appointment.Status == "Completada" || appointment.Status == "Cancelada")
                return false;

           
            // Validar estados permitidos si deseas
            var validStatuses = new[] { "Pendiente", "Completada", "Cancelada" };
            if (!validStatuses.Contains(dto.Status)) return false;



            appointment.Status = dto.Status;
            return await _repository.SaveChangesAsync();
        }


        public async Task<bool> RescheduleAppointmentAsync(UpdateAppointmentDto dto)
        {
            var appointment = await _repository.GetByIdAsync(dto.Id);

            if (appointment == null) return false;

            if (appointment.Status == "Completada" || appointment.Status == "Cancelada")
                return false;

            // Convertir la nueva fecha a UTC
            var newAppointmentUtc = DateTime.SpecifyKind(dto.NewAppointmentDate, DateTimeKind.Utc);

            if (newAppointmentUtc <= DateTime.UtcNow)
                return false;

            // Comparar solo por la hora en UTC
            var newHourUtc = newAppointmentUtc.TimeOfDay;

            // Validar conflicto con el doctor
            var hasConflict = await _repository.ExistsAsync(
                a => a.DoctorId == appointment.DoctorId &&
                     a.AppointmentDate.TimeOfDay == newHourUtc &&
                     a.Id != appointment.Id);

            if (hasConflict) return false;

            // Validar conflicto con el paciente
            var patientConflict = await _repository.ExistsAsync(
                a => a.PatientId == appointment.PatientId &&
                     a.AppointmentDate.TimeOfDay == newHourUtc &&
                     a.Id != appointment.Id);

            if (patientConflict) return false;

            // Reprogramar guardando en UTC
            appointment.AppointmentDate = newAppointmentUtc;
            return await _repository.SaveChangesAsync();
        }

        public async Task<List<string>> GetAvailableHoursAsync(Guid doctorId, DateTime date)
        {
            TimeSpan start, end;

            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                start = new TimeSpan(9, 0, 0);
                end = new TimeSpan(13, 0, 0);
            }
            else if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                return new List<string>();
            }
            else
            {
                start = new TimeSpan(8, 0, 0);
                end = new TimeSpan(16, 30, 0);
            }

            var allSlots = new List<DateTime>();
            var localDate = date.Date;

            for (var t = start; t <= end; t = t.Add(new TimeSpan(0, 30, 0)))
            {
                var localDateTime = localDate.Add(t);
                var utcDateTime = DateTime.SpecifyKind(localDateTime, DateTimeKind.Utc);
                allSlots.Add(utcDateTime);
            }

            var bookedAppointments = await _repository.GetAppointmentsByDoctorAndDateAsync(doctorId, date);

            var availableSlots = allSlots
                .Where(slot => !bookedAppointments.Contains(slot))
                .Select(slot => slot.ToString("HH:mm"))
                .ToList();

            return availableSlots;
        }

    }
}
