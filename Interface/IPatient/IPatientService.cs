using MediAgenda.DTOs.Patient;

namespace MediAgenda.Interface.IPatient
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDto>> GetAllAsync();
        Task<PatientDto?> GetByUserIdAsync(Guid userId);
        Task<bool> CreateAsync(CreatePatientDto createPatientDto);
        Task<bool> UpdateAsync(Guid userId, UpdatePatientDto updatePatientDto);
        Task<bool> DeleteAsync(Guid userId);
    }
}
