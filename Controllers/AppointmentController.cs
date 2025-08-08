using MediAgenda.DTOs.Appointment;
using MediAgenda.DTOs.Reception;
using MediAgenda.DTOs.Roles;
using MediAgenda.Entities;
using MediAgenda.Interface.IAppointment;
using MediAgenda.Responses;
using MediAgenda.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediAgenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _service;

        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            if (result == null)
            {
                return NotFound(
                 new ApiResponse<AppointmentDto>
                 {
                     Message = "Cita no encontrada",
                     Success = false,
                     Data = null
                 });
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(
                 new ApiResponse<AppointmentDto>
                 {
                     Message = "Cita no encontrada",
                     Success = false,
                     Data = null
                 });
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentDto dto)
        {
            var success = await _service.CreateAsync(dto);
            if (!success)
            {
                return BadRequest(
                new ApiResponse<CreateAppointmentDto>
                {
                    Message = "Ya existe una cita en ese horario.",
                    Success = false,
                    Data = dto
                });
            }
            return Ok(new ApiResponse<ReceptionDto> 
            {
                Message = "Cita creada correctamente",
                Success = true,
                Data = null
            });
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateAppointmentStatusDto dto)
        {
            var result = await _service.UpdateAppointmentStatusAsync(dto);
            if (!result)
            {
                return BadRequest(
                 new ApiResponse<UpdateAppointmentStatusDto>
                 {
                     Message = "No se pudo actualizar el estado de la cita.",
                     Success = false,
                     Data = dto
                 });
            }
            return Ok(
                 new ApiResponse<UpdateAppointmentStatusDto>
                 {
                     Message = "Estado de la cita actualizado correctamente.",
                     Success = true,
                     Data = dto
                 });
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctor(Guid doctorId)
        {
            var result = await _service.GetByDoctorAsync(doctorId);
            if (result == null)
            {
                return BadRequest(
                new ApiResponse<ReceptionDto>
                {
                    Message = "No encontrado",
                    Success = false,
                    Data = null
                });
            }
            return Ok(result);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatient(Guid patientId)
        {
            var result = await _service.GetByPatientAsync(patientId);
            if (result == null)
            {
                return BadRequest(
                new ApiResponse<ReceptionDto>
                {
                    Message = "No encontrado",
                    Success = false,
                    Data = null
                });
            }
            return Ok(result);
        }

        [HttpPut("reschedule")]
        public async Task<IActionResult> RescheduleAppointment([FromBody] UpdateAppointmentDto dto)
        {
            var result = await _service.RescheduleAppointmentAsync(dto);
            if (!result)
                return BadRequest("No se pudo reprogramar la cita. Verifique que no esté completada, cancelada o que el horario esté disponible.");

            return Ok("Cita reprogramada exitosamente.");
        }
    }
}
