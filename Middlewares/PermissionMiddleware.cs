using MediAgenda.Interface.IUserPermission;
using MediAgenda.Security;
using Microsoft.AspNetCore.Http;

namespace MediAgenda.Middlewares
{
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next; 

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserPermissionRepository permissionRepo)
        {
            var endpoint = context.GetEndpoint();
            var attribute = endpoint?.Metadata.GetMetadata<HasPermissionAttribute>();

            if (attribute != null)
            {
                var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "nameid");               
                if (userIdClaim == null)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Usuario sin permiso (claim inválido)");
                    return;
                }

                var userId = Guid.Parse(userIdClaim.Value);
                var hasPermission = await permissionRepo.HasPermissionAsync(userId, attribute.PermissionName);

                if (!hasPermission)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return;
                }
            }

            await _next(context);
        }
    }
}
