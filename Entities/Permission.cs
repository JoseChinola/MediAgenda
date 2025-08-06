using System.ComponentModel.DataAnnotations;

namespace MediAgenda.Entities
{
    public class Permission
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<UserPermission>? UserPermissions { get; set; }
    }
}
