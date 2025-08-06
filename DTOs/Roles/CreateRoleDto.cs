using System.ComponentModel.DataAnnotations;

namespace MediAgenda.DTOs.Roles
{
    public class CreateRoleDto    {
      
        [Required]
        public string Name { get; set; }

    }
}
