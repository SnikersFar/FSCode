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
            var userRepository = context.HttpContext.RequestServices.GetService(typeof(UserRepository)) as UserRepository;

            var isAuth = context.HttpContext.User.Identity!.IsAuthenticated;
            if (!isAuth)
            {
                context.HttpContext.SignOutAsync();
                context.Result = new ForbidResult();
                return;
            }
            var claimId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")!.Value;
            if (claimId is null || !int.TryParse(claimId, out int id))
            {
                context.HttpContext.SignOutAsync();
                context.Result = new ForbidResult();
                return;
            }
            var user = userRepository!.GetById(id);
            if (user == null)
            {
                context.HttpContext.SignOutAsync();
                context.Result = new ForbidResult();
                return;
            }
            var token = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token");
            if (token is null || token.Value != user.Token || user.TokenTime < DateTime.UtcNow)
            {
                context.HttpContext.SignOutAsync();
                context.Result = new ForbidResult();
                return;
            }
#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
            var success = Enum.TryParse(typeof(UserRole), context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")!.Value, out object role);
#pragma warning restore CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
            if ((user.Role & _availableRoles) == 0)
            {
                context.Result = new RedirectToActionResult("Index", "Menu", null);
                return;
            }
        }
    }
}
