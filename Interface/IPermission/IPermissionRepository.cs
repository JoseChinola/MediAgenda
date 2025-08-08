using MediAgenda.Entities;

namespace MediAgenda.Interface.IPermission
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync();
        Task<Permission?> GetByIdAsync(Guid id);
        Task AddAsync(Permission permission);
        void Update(Permission permission);
        void Delete(Permission permission);
        Task<bool> ExistsByNameAsync(string normalizedName);
        Task<bool> SaveChangesAsync();
    }
}
