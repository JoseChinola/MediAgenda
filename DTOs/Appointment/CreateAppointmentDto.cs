namespace MediAgenda.DTOs.Appointment
{
    public class CreateAppointmentDto
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Notes { get; set; }        
    }
}
