using MediAgenda.DTOs.Permission;

namespace MediAgenda.Interface.IPermission
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDto>> GetAllAsync();
        Task<PermissionDto?> GetByIdAsync(Guid id);
        Task<PermissionDto> CreateAsync(CreatePermissionDto createPermissionDto);
        Task<PermissionDto?> UpdateAsync(Guid id, CreatePermissionDto updatePermissionDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
