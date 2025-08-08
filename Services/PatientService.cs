using AutoMapper;
using MediAgenda.DTOs.Patient;
using MediAgenda.Entities;
using MediAgenda.Interface.IPatient;
using MediAgenda.Interface.IUser;

namespace MediAgenda.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public PatientService(IPatientRepository patientRepository, IMapper mapper, IUserRepository userRepository)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<bool> CreateAsync(CreatePatientDto createPatientDto)
        {
            // Validar que el usuario exista y que su rol sea "Patient"
            var user = await _userRepository.GetByIdAsync(createPatientDto.UserId);
            if (user == null || user.Role?.Name != "Patient")
                return false;

            var patient = _mapper.Map<Patient>(createPatientDto);
            await _patientRepository.AddAsync(patient);
            return await _patientRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<PatientDto>> GetAllAsync()
        {
            var patients = await _patientRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PatientDto>>(patients);
        }

        public async Task<PatientDto?> GetByUserIdAsync(Guid userId)
        {
            var patient = await _patientRepository.GetByUserIdAsync(userId);
            return _mapper.Map<PatientDto?>(patient);
        }

        public async Task<bool> UpdateAsync(Guid userId, CreatePatientDto updatePatientDto)
        {
            var patient = await _patientRepository.GetByUserIdAsync(userId);
            if (patient == null) return false;

            _mapper.Map(updatePatientDto, patient);
            _patientRepository.Update(patient);
            return await _patientRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid userId)
        {
            var patient = await _patientRepository.GetByUserIdAsync(userId);
            if (patient == null) return false;

            _patientRepository.Delete(patient);
            return await _patientRepository.SaveChangesAsync();
        }
    }
}
