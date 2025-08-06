using System.ComponentModel.DataAnnotations;

namespace MediAgenda.DTOs.User
{
    public class CreateUserDto
    {      
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RoleId { get; set; }
    }
}
