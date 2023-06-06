using Data.Repositories.Interfaces;
using Domain.Entities;
using Domain.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Infrastructure.Filters.Autorization
{
    public class AutorizationByHashFilter : IAuthorizationFilter
    {
        private const string RoleClaimType = "Role";
        private const string HashPasswordClaimType = "HashPassword";

        private Roles _availableRoles;

        private readonly IRepository<UserModel> _userRepository;

        public AutorizationByHashFilter(Data.Repositories.Interfaces.IRepository<UserModel> userRepository, Roles roles)
        {
            _userRepository = userRepository;
            _availableRoles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAuth = context.HttpContext.User.Identity.IsAuthenticated;
            if (!isAuth)
            {
                context.Result = new ForbidResult();
                return;
            }
            var claimId = context.HttpContext.User.Claims.FirstOrDefault(c => c.ValueType == "Id");
            if (claimId is null || int.TryParse(claimId.Value, out int id))
            {
                context.Result = new ForbidResult();
                return;
            }
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                context.Result = new ForbidResult();
                return;
            }
            var claimHashPassword = context.HttpContext.User.Claims.FirstOrDefault(c => c.ValueType == "HashPassword");
            if (claimHashPassword is null || claimHashPassword.Value != user.HashPassword)
            {
                context.Result = new ForbidResult();
                return;
            }
            if ((user.Role & _availableRoles) == 0)
            {
                context.Result = new ForbidResult();
                return;
            } 
        }
    }
}
