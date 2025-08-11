using MediAgenda.DTOs.Permission;
using MediAgenda.DTOs.UserPermission;
using MediAgenda.Entities;

namespace MediAgenda.Interface.IUserPermission
{
    public interface IUserPermissionService
    {
        Task<bool> AssignPermission(UserPermissionDto userPermissionDto);
        Task<bool> RemovePermission(UserPermissionDto userPermissionDto);
        Task<IEnumerable<UserPermissionResponseDto>> GetUserPermissions(Guid userId);
        Task<bool> HasPermission(Guid userId, string permissionName);
    }
}
