using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediAgenda.Entities
{
    public class Doctor
    {
        [Key]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [Required]
        public string Specialty { get; set; }

        public string? Bio { get; set; }

        public User User { get; set; }

        public ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();
    }
}
