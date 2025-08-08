using MediAgenda.DTOs.User;
using MediAgenda.Entities;

namespace MediAgenda.Interface.IUser
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdWithRoleAsync(Guid id);
        Task<IEnumerable<User>> GetAllWithRolesAsync();
        Task AddAsync(User user);
        void Update(User user);
        void Delete(User user);
        Task<bool> SaveChangesAsync();
    }
}
