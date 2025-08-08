using MediAgenda.Entities;

namespace MediAgenda.Interface.IPatient
{
    public interface IPatientRepository
    {
        Task AddAsync(Patient patient);
        void Update(Patient patient);
        void Delete(Patient patient);
        Task<Patient?> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<bool> SaveChangesAsync();
    }
}
