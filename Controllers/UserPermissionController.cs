using MediAgenda.DTOs.Permission;
using MediAgenda.DTOs.UserPermission;
using MediAgenda.Interface.IUserPermission;
using MediAgenda.Responses;
using MediAgenda.Security;
using MediAgenda.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediAgenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPermissionController : ControllerBase
    {
        private readonly IUserPermissionService _userService;
        
        public UserPermissionController(IUserPermissionService userService)
        {
            _userService = userService;
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignPermission([FromBody] UserPermissionDto dto)
        {

            var assigned = await _userService.AssignPermission(dto);

            if (!assigned)
            {
                return BadRequest(new ApiResponse<UserPermissionDto>
                {
                    Message = "No se pudo asignar el permiso.",
                    Success = false,
                    Data = null
                });
            }

            return Ok(new ApiResponse<UserPermissionDto>
            {
                Message = "Permiso asignado correctamente",
                Success = true,
                Data = dto
            });
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemovePermission([FromBody] UserPermissionDto dto)
        {
            var removed = await _userService.RemovePermission(dto);
            if (!removed)
                return BadRequest(new ApiResponse<UserPermissionDto>
                {
                    Message = "No se pudo remover el permiso.",
                    Success = false,
                    Data = null
                });

            return Ok(new ApiResponse<UserPermissionDto>
            {
                Message = "Permiso removido correctamente",
                Success = true,
                Data = dto
            });
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserPermissions(Guid userId)
        {
            var permissions = await _userService.GetUserPermissions(userId);

            if (!permissions.Any())
            {
                return Ok(new ApiResponse<UserPermissionResponseDto>
                {
                    Message = "No se encontraron permisos",
                    Success = false,
                    Data = null
                });
            }

            return Ok(new ApiResponse<IEnumerable<UserPermissionResponseDto>>
            {
                Message = "Permisos obtenidos correctamente",
                Success = true,
                Data = permissions
            });
        }

        [HttpGet("test-permission/{userId}")]
        public async Task<IActionResult> TestPermission(Guid userId)
        {
            var hasPermission = await _userService.HasPermission(userId, "canGetUser");
            Console.WriteLine($"Esto es lo permisos {hasPermission}");
            return Ok(new { TienePermiso = hasPermission });
        }

      
    }
}
