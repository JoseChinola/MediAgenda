namespace MediAgenda.DTOs.Appointment
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string? Notes { get; set; }
    
    }
}