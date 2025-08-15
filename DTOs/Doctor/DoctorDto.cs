using MediAgenda.DTOs.User;

namespace MediAgenda.DTOs.Doctor
{
    public class DoctorDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Specialty { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string phoneNumber { get; set; }

    }
}
