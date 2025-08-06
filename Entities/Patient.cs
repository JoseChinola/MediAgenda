using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediAgenda.Entities
{
    public class Patient
    {
        [Key]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Address { get; set; }
        public User User { get; set; }
        public ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();

    }
}
