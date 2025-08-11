using MediAgenda.DTOs.UserPermission;
using MediAgenda.Entities;

namespace MediAgenda.Interface.IUserPermission
{
    public interface IUserPermissionRepository
    {
        Task<bool> AssignPermissionAsync(UserPermission userPermission);
        void RemovePermissionAsync(UserPermission userPermission);
        Task<IEnumerable<UserPermission>> GetUserPermissionsAsync(Guid userId);
        Task<bool> HasPermissionAsync(Guid userId, string PermissionName);
        Task SaveChangesAsync();
    }
}
