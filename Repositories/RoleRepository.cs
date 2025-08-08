using MediAgenda.Data;
using MediAgenda.Entities;
using MediAgenda.Interface.IRole;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MediAgenda.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public void Delete(Role role)
        {
            _context.Roles.Remove(role);
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetByIdAsync(Guid id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public void Update(Role role)
        {
            _context.Roles.Update(role);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
