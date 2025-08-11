using MediAgenda.Entities;
using MediAgenda.Interface.IUserPermission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace MediAgenda.Security
{
    public class HasPermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _permissionName;

        public string PermissionName => _permissionName;

        public HasPermissionAttribute(string permissionName)
        {
            _permissionName = permissionName;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                context.Result = new ForbidResult();
                return;
            }


            var permissionRepo = context.HttpContext.RequestServices.GetRequiredService<IUserPermissionRepository>();


            // Validar permiso
            var hasPermission = await permissionRepo.HasPermissionAsync(userId, PermissionName);

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
