using MediAgenda.DTOs.Doctor;

namespace MediAgenda.Interface.IDoctor
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDto>> GetAllAsync();
        Task<DoctorDto?> GetByIdAsync(Guid userId);
        Task<DoctorDto?> CreateAsync(CreateDoctorDto createDoctorDto);
        Task<bool> UpdateAsync(Guid userId, CreateDoctorDto updateDoctorDto);
        Task<bool> DeleteAsync(Guid userId);
    }
}
