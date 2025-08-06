using System.ComponentModel.DataAnnotations;

namespace MediAgenda.Entities
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<User>? Users { get; set; }

    }
}
