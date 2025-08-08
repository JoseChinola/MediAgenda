using AutoMapper;
using MediAgenda.DTOs.Auth;
using MediAgenda.DTOs.User;
using MediAgenda.Entities;
using MediAgenda.Interface;
using MediAgenda.Interface.IUser;
using System.Security.Cryptography;
using System.Text;

namespace MediAgenda.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _jwtTokenService;

        public UserService(IUserRepository userRepository, IMapper mapper, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
        }


        public async Task<UserDto> CreateAsync(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);

            // Hash password
            user.PasswordHash = ComputeSha256Hash(createUserDto.Password);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var userWithRole = await _userRepository.GetByIdAsync(user.Id);

            return _mapper.Map<UserDto>(userWithRole);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            _userRepository.Delete(user);
            return await _userRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();            
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return _mapper.Map<UserDto?>(user);
        }

        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto?>(user);
        }

        public async Task<bool> UpdateAsync(Guid id, CreateUserDto updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if(user == null) return false;

            _mapper.Map(updateUserDto, user);

            // Update password hash if password provided
            if (!string.IsNullOrWhiteSpace(updateUserDto.Password))
            {
                user.PasswordHash = ComputeSha256Hash(updateUserDto.Password);
            }


            _userRepository.Update(user);
            return await _userRepository.SaveChangesAsync();
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);

            if (user == null) return null;

            var hashedPassword = ComputeSha256Hash(loginDto.Password);
            if (user.PasswordHash != hashedPassword) return null;

            var token = _jwtTokenService.GenerateToken(user.Id, user.Role?.Name ?? "Usuario");

            return new AuthResponseDto
            {
                Token = token,
                FullName = $"{user.FirstName} {user.LastName}",
                Role = user.Role?.Name ?? "Usuario"
            };
        }

        public async Task<bool> ChangePasswordAsync(Guid id, ChangePasswordDto changePasswordDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            // Verify old password
            var hashedOldPassword = ComputeSha256Hash(changePasswordDto.CurrentPassword);

            if (user.PasswordHash != hashedOldPassword) return false;

            // Update to new password
            user.PasswordHash = ComputeSha256Hash(changePasswordDto.NewPassword);
            _userRepository.Update(user);
            return await _userRepository.SaveChangesAsync();
        }




        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (var sha256Hash = SHA256.Create())
            {
                // ComputeHash returns byte array  
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                var builder = new StringBuilder();
                foreach (var t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
