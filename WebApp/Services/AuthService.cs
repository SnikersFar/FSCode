using AutoMapper;
using Domain.Entities;
using Infrastructure.Contexts.Repositories;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using WebApp.Dtos.Auth;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class AuthService
    {
        private AuthUserRepository _authUserRepository;
        private readonly IConfiguration _configuration;
        private IHttpContextAccessor _contextAccessor;
        private IMapper _mapper;
        public AuthService(
            BaseRepository<AuthUser> userRepository,
            IConfiguration configuration,
            IHttpContextAccessor contextAccessor,
            IMapper mapper)
        {
            _authUserRepository = (userRepository as AuthUserRepository)!;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }

        public async ValueTask<AuthorizationViewModel> LoginAsync(AuthRequestDto authDto)
        {
            var authViewModel = new AuthorizationViewModel();

            var user = _authUserRepository.GetUserBy(user => user.Login == authDto.Login);
            if (user is null || user.HashPassword != authDto.Password.ToHash())
            {
                authViewModel.AuthResponse = new AuthResponseDto() { Error = "Not valid login or password." };
                return authViewModel;
            }
            await AuthorizeClientAsync(user);
            return new AuthorizationViewModel() { AuthResponse = new AuthResponseDto() { Success = true, } };

        }
        public async Task<RegistrationViewModel> RegisterAsync(RegRequestDto regDto)
        {
            var authViewModel = new RegistrationViewModel();
            #region UserValidation
            var user = _authUserRepository.GetUserBy(user => user.Login == regDto.Login!);
            if (user is not null)
            {
                authViewModel.RegResponse = new RegResponseDto() { Error = "Login is not available." };
                return authViewModel;
            }
            user = _authUserRepository.GetUserBy(user => user.Email == regDto.Email!);
            if (user is not null)
            {
                authViewModel.RegResponse = new RegResponseDto() { Error = "This email is already taken." };
                return authViewModel;
            }
            #endregion
            user = _mapper.Map<AuthUser>(authViewModel);
            await AuthorizeClientAsync(user!);
            return new RegistrationViewModel() { RegResponse = new RegResponseDto() { Success = true, } };

        }
        private async Task<bool> AuthorizeClientAsync(AuthUser user)
        {
            user.Token = Guid.NewGuid().ToString();
            user.TokenTime = DateTime.UtcNow.AddDays(int.Parse(_configuration["Auth:TokenTimeDays"]!));
            await _authUserRepository.SaveAsync(user);
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Login", user.Login),
                new Claim("Token",user.Token!),
            };
            var claimsIdentity = new ClaimsIdentity(claims, _configuration["Auth:AuthCookies"]);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await _contextAccessor.HttpContext!.SignInAsync(claimsPrincipal);
            return true;
        }
        public async Task LogoutAsync()
        {
            var isUserId = int.TryParse(_contextAccessor.HttpContext!.User.Claims
                                 .SingleOrDefault(c => c.Type == "Id")?.Value, out int userId);
            if (isUserId)
            {
                var user = _authUserRepository.GetById(userId);
                user.Token = null;
                user.TokenTime = null;
                await _authUserRepository.SaveAsync(user);

            }
            _ = _contextAccessor.HttpContext!.SignOutAsync();
        }
    }
}
