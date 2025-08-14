namespace MediAgenda.DTOs.Appointment
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public string Doctor { get; set; }
        public string Patient { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string? Notes { get; set; }
    
    }
}