using MediAgenda.Data;
using MediAgenda.Entities;
using MediAgenda.Interface.IPermission;
using Microsoft.EntityFrameworkCore;

namespace MediAgenda.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public PermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
        }

        public void Delete(Permission permission) 
        {
            _context.Permissions.Remove(permission);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Permissions
                .AnyAsync(p => p.Name.ToLower() == name);
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await _context.Permissions
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Permission?> GetByIdAsync(Guid id)
        {
            return await _context.Permissions
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Update(Permission permission) 
        {
            _context.Permissions.Update(permission);
        }

        public async Task<bool> Existsbynameasync(string normalizedName)
        {
            return await _context.Permissions
                .AnyAsync(p => p.Name.ToLower() == normalizedName);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
