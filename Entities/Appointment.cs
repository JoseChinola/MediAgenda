using System.ComponentModel.DataAnnotations;

namespace MediAgenda.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }
        [Required]
        public Guid DoctorId { get; set; }
        [Required]
        public Guid PatientId { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = "Pendiente";
        public string? Notes { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }

    }
}
