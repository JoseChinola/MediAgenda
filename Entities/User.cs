using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace MediAgenda.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set;  }


        public string? FirstName { get; set; }

       
        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

   
        public string? PasswordHash { get; set; }

       
        public Guid? RoleId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // relaciones
        [ForeignKey("RoleId")]
        public Role? Role { get; set; }

        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }

        public ICollection<UserPermission>? UserPermissions { get; set; }
    }
}
