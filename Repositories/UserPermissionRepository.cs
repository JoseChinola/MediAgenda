using MediAgenda.Data;
using MediAgenda.Entities;
using MediAgenda.Interface.IUserPermission;
using Microsoft.EntityFrameworkCore;

namespace MediAgenda.Repositories
{
    public class UserPermissionRepository : IUserPermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public UserPermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }    
          
        public async Task<bool> AssignPermissionAsync(UserPermission userPermission)
        {
            var permissionExists = await _context.Permissions.AnyAsync(p => p.Id == userPermission.PermissionId);
            if (!permissionExists)
                return false;

            await _context.UserPermissions.AddAsync(userPermission);
            return true;
        }

        public void RemovePermissionAsync(UserPermission userPermission)
        {
           _context.UserPermissions.Remove(userPermission);
        }

        public async Task<IEnumerable<UserPermission>> GetUserPermissionsAsync(Guid userId)
        {
            return await _context.UserPermissions
                .Include(up => up.User)
                .Include(up => up.Permission)
                .Where(up => up.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> HasPermissionAsync(Guid userId, string permissionName)
        {
            return await _context.UserPermissions
               .Include(up => up.Permission)
               .AnyAsync(up => up.UserId == userId && up.Permission.Name.ToLower() == permissionName.ToLower());
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
