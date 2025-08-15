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

            if (dto.NewAppointmentDate <= DateTime.Now)
                return false;

            var hasConflict = await _repository.ExistsAsync(
             a => a.DoctorId == appointment.DoctorId &&
             a.AppointmentDate == dto.NewAppointmentDate &&
             a.Id != appointment.Id);

            if (hasConflict) return false;

            var patientConflict = await _repository.ExistsAsync(
             a => a.PatientId == appointment.PatientId &&
             a.AppointmentDate == dto.NewAppointmentDate &&
             a.Id != appointment.Id);

            if (patientConflict) return false;

            appointment.AppointmentDate = dto.NewAppointmentDate;
            return await _repository.SaveChangesAsync();
        }
    }
}
