using MediAgenda.DTOs.Doctor;
using MediAgenda.DTOs.Roles;
using MediAgenda.DTOs.User;
using MediAgenda.Entities;
using MediAgenda.Interface.IDoctor;
using MediAgenda.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediAgenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService) 
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _doctorService.GetAllAsync();           
            return Ok(doctors);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var doctor = await _doctorService.GetByIdAsync(userId);
            if (doctor == null)
            {
                return NotFound(new ApiResponse<RoleDto>
                {
                    Message = "Doctor no encontrado",
                    Success = false,
                    Data = null
                });
            }

            return Ok(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDoctorDto createDoctorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdDoctor = await _doctorService.CreateAsync(createDoctorDto);

            var response = new ApiResponse<DoctorDto>
            {
                Message = "Doctor creado",
                Success = true,
                Data = createdDoctor
            };

            return CreatedAtAction(nameof(GetById), new { userId = createdDoctor.UserId }, response);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(Guid userId, [FromBody] CreateDoctorDto updateDoctorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _doctorService.UpdateAsync(userId, updateDoctorDto);
            if (!result)
            {
                return NotFound(new ApiResponse<RoleDto>
                {
                    Message = "Doctor no encontrado",
                    Success = false,
                    Data = null
                });
            }

            return Ok(new ApiResponse<UserDto>
            {
                Success = true,
                Message = "Doctor actualizado correctamente",
                Data = null
            });
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var result = await _doctorService.DeleteAsync(userId);
            if (!result)
            {
                return NotFound(new ApiResponse<RoleDto>
                {
                    Message = "Doctor no encontrado",
                    Success = false,
                    Data = null
                });
            }

            return Ok(new ApiResponse<UserDto>
            {
                Success = true,
                Message = "Doctor eliminado correctamente",
                Data = null
            });
        }
    }
}
