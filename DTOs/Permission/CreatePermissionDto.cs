using System.ComponentModel.DataAnnotations;

namespace MediAgenda.DTOs.Permission
{
    public class CreatePermissionDto
    {
        [Required]
        public string Name { get; set; }
    }
}
