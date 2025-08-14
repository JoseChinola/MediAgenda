using MediAgenda.DTOs.Appointment;
using MediAgenda.DTOs.User;

namespace MediAgenda.DTOs.Patient
{
    public class PatientDto
    {
        public Guid UserId { get; set; }
        public DateTime BirthDate { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public UserDto User { get; set; } = new UserDto();
    }
}
