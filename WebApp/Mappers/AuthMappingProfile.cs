using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Extensions;
using Newtonsoft.Json;
using WebApp.Dtos.Auth;

namespace WebApp.Mappers
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<RegRequestDto, User>()
                .ForMember(to => to.HashPassword, mce => mce.MapFrom(from => from.Password!.ToHash()))
                .ForMember(to => to.Role, mce => mce.MapFrom(from => UserRole.None));
        }
    }
}
