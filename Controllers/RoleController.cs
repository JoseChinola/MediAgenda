using MediAgenda.DTOs.Roles;
using MediAgenda.Interface;
using MediAgenda.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MediAgenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> Get()
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetById(Guid id)
        {
            var role = await _roleService.GetByIdAsync(id);
            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<RoleDto>> Create(CreateRoleDto createRoleDto)
        {
            var role = await _roleService.CreateAsync(createRoleDto);

            var response = new ApiResponse<RoleDto>
            {
                Message = "Rol creado correctamente",
                Success = true,
                Data = role
            };

            return CreatedAtAction(nameof(GetById), new { id = role.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CreateRoleDto updateRoleDto)
        {
            var updated = await _roleService.UpdateAsync(id, updateRoleDto);
            if (updated == null)
            {
                return NotFound(new ApiResponse<RoleDto>
                {
                    Success = false,
                    Message = "Rol no encontrado",
                    Data = null
                });
            }
            return Ok(new ApiResponse<RoleDto>
            {
                Success = true,
                Message = "Rol actualizado correctamente",
                Data = updated
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _roleService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound(new ApiResponse<RoleDto>
                {
                    Success = false,
                    Message = "Rol no encontrado",
                    Data = null
                });
            }
            return Ok(new ApiResponse<RoleDto>
            {
                Success = true,
                Message = "Rol eliminado correctamente",
                Data = null
            });
        }
    }
}
