using MediAgenda.DTOs.Roles;

namespace MediAgenda.Interface
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllAsync();
        Task<RoleDto?> GetByIdAsync(Guid id);
        Task<RoleDto> CreateAsync(CreateRoleDto createRoleDto);
        Task<RoleDto> UpdateAsync(Guid id, CreateRoleDto updateRoleDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
