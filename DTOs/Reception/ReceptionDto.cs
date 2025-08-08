namespace MediAgenda.DTOs.Reception
{
    public class ReceptionDto
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        public string? RoleName { get; set; }
    }
}
