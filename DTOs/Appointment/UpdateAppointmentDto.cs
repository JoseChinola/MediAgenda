namespace MediAgenda.DTOs.Appointment
{
    public class UpdateAppointmentDto
    {
        public Guid Id { get; set; }
        public DateTime NewAppointmentDate { get; set; }
    }
}
