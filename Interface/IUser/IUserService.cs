using MediAgenda.DTOs.Auth;
using MediAgenda.DTOs.User;

namespace MediAgenda.Interface.IUser
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(Guid id);
        Task<UserDto?> GetByEmailAsync(string email);
        Task<UserDto> CreateAsync(CreateUserDto createUserDto);
        Task<bool> UpdateAsync(Guid id, UpdateUserDto updateUserDto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ChangePasswordAsync(Guid id, ChangePasswordDto changePasswordDto);
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
    }
}
