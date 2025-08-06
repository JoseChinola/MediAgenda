using MediAgenda.DTOs.Auth;

namespace MediAgenda.Interface
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
    }
}
