namespace MediAgenda.DTOs.Appointment
{
    public class UpdateAppointmentStatusDto
    {
        public Guid AppointmentId { get; set; }
        public string Status { get; set; }
    }
}
