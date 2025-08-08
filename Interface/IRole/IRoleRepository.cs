using MediAgenda.Entities;

namespace MediAgenda.Interface.IRole
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role?> GetByIdAsync(Guid id);
        Task AddAsync(Role role);
        void Update(Role role);
        void Delete(Role role);
        Task<bool> SaveChangesAsync();
    }
}
