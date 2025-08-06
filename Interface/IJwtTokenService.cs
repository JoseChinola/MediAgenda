namespace MediAgenda.Interface
{
    public interface IJwtTokenService
    {
        public string GenerateToken(Guid userId, string role);
    }
}
