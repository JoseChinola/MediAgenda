using MediAgenda.DTOs.Auth;

namespace MediAgenda.Interface.IUser
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
    }
}
