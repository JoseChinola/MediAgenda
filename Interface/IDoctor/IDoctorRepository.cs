using MediAgenda.DTOs.Doctor;
using MediAgenda.Entities;

namespace MediAgenda.Interface.IDoctor
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(Guid userId);
        Task AddAsync(Doctor doctor);
        void Update(Doctor doctor);
        void Delete(Doctor doctor);
        Task<bool> SaveChangesAsync();
    }
}
