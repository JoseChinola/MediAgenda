using MediAgenda.Data;
using MediAgenda.Entities;
using MediAgenda.Interface.IPatient;
using Microsoft.EntityFrameworkCore;

namespace MediAgenda.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;
        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
        }

        public void Update(Patient patient)
        {
            _context.Patients.Update(patient);
        }

        public void Delete(Patient patient)
        {
            _context.Patients.Remove(patient);
        }


        public async Task<Patient?> GetByUserIdAsync(Guid userId)
        {
            return await _context.Patients
                .Include(p => p.User)
                .ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _context.Patients
                .Include(p => p.User)
                .ThenInclude(u => u.Role)
                .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
