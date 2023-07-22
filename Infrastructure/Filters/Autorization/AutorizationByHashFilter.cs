using Domain.Enums;
using Infrastructure.Contexts.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Filters.Autorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeByTokenFilter : Attribute, IAuthorizationFilter
    {
        private readonly UserRole _availableRoles;
        public AuthorizeByTokenFilter(UserRole userRoles)
        {
            _availableRoles = userRoles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userRepository = context.HttpContext.RequestServices.GetService(typeof(AuthUserRepository)) as AuthUserRepository;

            var isAuth = context.HttpContext.User.Identity!.IsAuthenticated;
            if (!isAuth)
            {
                UnAuthorize(context);
                return;
            }
            var claimId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")!.Value;
            if (claimId is null || !int.TryParse(claimId, out int id))
            {
                UnAuthorize(context);
                return;
            }
            var user = userRepository!.GetById(id);
            if (user == null)
            {
                UnAuthorize(context);
                return;
            }
            var token = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token");
            if (token is null || token.Value != user.Token || user.TokenTime < DateTime.UtcNow)
            {
                UnAuthorize(context);
                return;
            }

#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.

            var isRole = Enum.TryParse(typeof(UserRole),
                          context.HttpContext.User.Claims
                          .FirstOrDefault(c => c.Type == "Id")!.Value, out object role);

#pragma warning restore CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.

            if (!isRole || (user.Role & _availableRoles) == 0)
            {
                context.Result = new RedirectToActionResult("Index", "Menu", null);
                return;
            }
        }
        private static void UnAuthorize(AuthorizationFilterContext context)
        {
            context.HttpContext.SignOutAsync();
            context.Result = new UnauthorizedResult();
        }
    }
}
