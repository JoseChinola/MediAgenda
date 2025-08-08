namespace MediAgenda.DTOs.Patient
{
    public class CreatePatientDto
    {
        public Guid UserId { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Address { get; set; }
    }
}
