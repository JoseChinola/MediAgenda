using MediAgenda.DTOs.Auth;
using MediAgenda.DTOs.Roles;
using MediAgenda.DTOs.User;
using MediAgenda.Interface;
using MediAgenda.Interface.IUser;
using MediAgenda.Responses;
using MediAgenda.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace MediAgenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenService _jwtTokenService;

        public UserController(IUserService userService, IJwtTokenService jwtTokenService)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _userService.LoginAsync(loginDto);
            if (result == null) return Unauthorized("Credenciales incorrectas");

            return Ok(result);
        }



        [HasPermission("canGetUser")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if(user == null)
            {
                return NotFound(
                 new ApiResponse<RoleDto>
                 {
                     Message = "Usuario no encontrado",
                     Success = false,
                     Data = null
                 });
            }
            return Ok(user);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userService.GetByEmailAsync(email);
            if(user == null)
            {
                return NotFound(
                 new ApiResponse<RoleDto>
                 {
                     Message = "Usuario no encontrado",
                     Success = false,
                     Data = null
                 });
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var createdUser = await _userService.CreateAsync(createUserDto);

            var response = new ApiResponse<UserDto>
            {
                Message = "Usuario creado correctamente",
                Success = true,
                Data = createdUser
            };
            return CreatedAtAction(nameof(GetById), new {id = createdUser.Id }, response);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateUserDto updateUserDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _userService.UpdateAsync(id, updateUserDto);
            if(!result)
            {
                return NotFound(
                 new ApiResponse<RoleDto>
                 {
                     Message = "Usuario no encontrado",
                     Success = false,
                     Data = null
                 });
            };

            return Ok(new ApiResponse<UserDto>
            {
                Success = true,
                Message = "Usuario actualizado correctamente",
                Data = null
            });
        }

        [HttpPut("change-password/{id}")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
            {
                return Unauthorized(new ApiResponse<string>
                {
                    Message = "Usuario no autenticado",
                    Success = false,
                    Data = null
                });
            }

            var result = await _userService.ChangePasswordAsync(Guid.Parse(userId), changePasswordDto);
            if (!result)
            {
                return NotFound(
                 new ApiResponse<RoleDto>
                 {
                     Message = "Usuario no encontrado",
                     Success = false,
                     Data = null
                 });
            };

            return Ok(new ApiResponse<RoleDto>
            {
                Success = true,
                Message = "Contraseña cambiada correctamente",
                Data = null
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.DeleteAsync(id);
            if(!result)
            {
                return NotFound(
                 new ApiResponse<RoleDto>
                 {
                     Message = "Usuario no encontrado",
                     Success = false,
                     Data = null
                 });
            };

            return Ok(new ApiResponse<RoleDto>
            {
                Success = true,
                Message = "Usuario eliminado correctamente",
                Data = null
            });
        }

    }
}
