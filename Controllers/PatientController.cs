using MediAgenda.DTOs.Patient;
using MediAgenda.DTOs.Roles;
using MediAgenda.Interface.IPatient;
using MediAgenda.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MediAgenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _patientService.GetAllAsync();
            return Ok(patients);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var patient = await _patientService.GetByUserIdAsync(userId);
            if(patient == null)
            {
                return NotFound(
                 new ApiResponse<PatientDto>
                 {
                     Message = "Paciente no encontrado",
                     Success = false,
                     Data = null
                 });
            }

            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePatientDto createPatientDto)
        {
            var result = await _patientService.CreateAsync(createPatientDto);
            if (!result)
            {
                return BadRequest(
                   new ApiResponse<PatientDto>
                   {
                       Message = "No se pudo crear el paciente.",
                       Success = false,
                       Data = null
                   });
            }

            return Ok(
               new ApiResponse<PatientDto>
               {
                   Message = "Paciente creado correctamente",
                   Success = true,
                   Data = null
               });
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(Guid userId, [FromBody] CreatePatientDto updatePatientDto)
        {
            var result = await _patientService.UpdateAsync(userId, updatePatientDto);

            if (!result)
            {
                return NotFound(
                   new ApiResponse<PatientDto>
                   {
                       Message = "No se encontro el paciente.",
                       Success = false,
                       Data = null
                   });
            }

            return Ok(
               new ApiResponse<PatientDto>
               {
                   Message = "Paciente actualizado correctamente",
                   Success = true,
                   Data = null
               });
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var result = await _patientService.DeleteAsync(userId);
            if (!result)
            {
                return NotFound(
                   new ApiResponse<PatientDto>
                   {
                       Message = "No se encontro el paciente.",
                       Success = false,
                       Data = null
                   });
            }

            return Ok(
              new ApiResponse<PatientDto>
              {
                  Message = "Paciente eliminado correctamente",
                  Success = true,
                  Data = null
              });

        }

    }
}
