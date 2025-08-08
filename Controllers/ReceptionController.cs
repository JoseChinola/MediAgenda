using MediAgenda.DTOs.Reception;
using MediAgenda.DTOs.Roles;
using MediAgenda.Interface.Reception;
using MediAgenda.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediAgenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptionController : ControllerBase
    {
        private readonly IReceptionService _receptionService;

        public ReceptionController(IReceptionService receptionService)
        {
            _receptionService = receptionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceptionDto>>> GetAll()
        {
            var result = await _receptionService.GetAllAsync();
            if(result == null)
            {
                return NotFound(
                 new ApiResponse<ReceptionDto>
                 {
                     Message = "Usuario no encontrado",
                     Success = false,
                     Data = null
                 });
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReceptionDto>> GetById(Guid id)
        {
            var result = await _receptionService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound(
                 new ApiResponse<ReceptionDto>
                 {
                     Message = "Usuario no encontrado",
                     Success = false,
                     Data = null
                 });
            }
            return Ok(result);
        }
    }
}
