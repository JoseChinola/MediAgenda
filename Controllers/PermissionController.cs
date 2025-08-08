using MediAgenda.DTOs.Permission;
using MediAgenda.Interface.IPermission;
using MediAgenda.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MediAgenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _service;

        public PermissionController(IPermissionService service) 
        {
            _service = service;
        } 

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            if(result == null)
            {
                return NotFound(
                new ApiResponse<PermissionDto>
                {
                    Message = "No se encontro permisos",
                    Success = false,
                    Data = null
                });
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
               return NotFound(
               new ApiResponse<PermissionDto>
               {
                   Message = "No se encontro permisos",
                   Success = false,
                   Data = null
               });
            } 
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePermissionDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            if (created == null)
            {
                return Conflict(new ApiResponse<string>
                {
                    Message = "Ya existe un permiso con ese nombre",
                    Success = false,
                    Data = null
                });
            }


            return CreatedAtAction(nameof(GetById), 
                new {id = created?.Id}, 
                new ApiResponse<PermissionDto>
                {
                    Message = "Permiso creado correctamente",
                    Success = true,
                    Data = created
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreatePermissionDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, dto);
            if(updated == null)
            {             
                    return NotFound(
                    new ApiResponse<PermissionDto>
                    {
                        Message = "No se encontro permisos",
                        Success = false,
                        Data = null
                    });   
            }
            return Ok(new ApiResponse<PermissionDto>
            {
                Message = "Permiso actualizado",
                Success = true,
                Data = updated
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var removed = await _service.DeleteAsync(id);
            if (!removed)
            {
                return NotFound(
                new ApiResponse<PermissionDto>
                {
                    Message = "No se encontro permisos",
                    Success = false,
                    Data = null
                });
            }

            return Ok(new ApiResponse<PermissionDto>
            {
                Message = "Permiso eliminado",
                Success = true,
                Data = null
            });
        }        
    }
}
