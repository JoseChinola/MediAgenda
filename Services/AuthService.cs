using MediAgenda.DTOs.Auth;
using MediAgenda.Interface;
using System.Security.Cryptography;
using System.Text;

namespace MediAgenda.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _tokenService;

        public AuthService(IUserRepository userRepository, IJwtTokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }


        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if(user == null) return null;

            var hashedPassword = ComputeSha256Hash(loginDto.Password);
            if (user.PasswordHash != hashedPassword) return null;

            var token = _tokenService.GenerateToken(user.Id, user.Role?.Name ?? "Usuario");

            return new AuthResponseDto
            {
                Token = token,         
                Role = user.Role?.Name ?? "Sin Rol"
            };
        }


        private string ComputeSha256Hash(string rawData)
        {
            using var sha256Hash = SHA256.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var builder = new StringBuilder();
            foreach (var t in bytes) builder.Append(t.ToString("x2"));
            return builder.ToString();
        }
    }
}
