namespace MediAgenda.DTOs.UserPermission
{
    public class UserPermissionResponseDto
    {
        public Guid UserId { get; set; }
        public string? Fullname { get; set; }
        public Guid PermissionId { get; set; }
        public string? PermissionName { get; set; }
    }
}
