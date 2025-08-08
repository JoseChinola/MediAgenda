using MediAgenda.Data;
using MediAgenda.Entities;
using MediAgenda.Interface.IDoctor;
using Microsoft.EntityFrameworkCore;

namespace MediAgenda.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;
        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
        }

        public void Delete(Doctor doctor)
        {
            _context.Doctors.Remove(doctor);
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors
                .Include(d => d.User)
                .ThenInclude(u => u.Role)
                .ToListAsync();
        }

        public async Task<Doctor?> GetByIdAsync(Guid userId)
        {
            return await _context.Doctors
                .Include(d => d.User)
                .ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(d => d.UserId == userId);
        }

        public void Update(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
