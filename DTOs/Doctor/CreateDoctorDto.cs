using System.ComponentModel.DataAnnotations;

namespace MediAgenda.DTOs.Doctor
{
    public class CreateDoctorDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Specialty { get; set; }
        public string? Bio { get; set; }
    }
}
