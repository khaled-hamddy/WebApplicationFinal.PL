using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebApplicationFinal.PL.ViewModels;

namespace WebApplicationFinal.PL.Helpers
{
    public class RoleProfile:Profile
    {
        public RoleProfile() 
        {
            CreateMap<RoleViewModel, IdentityRole>()
                    .ForMember(d => d.Name, o => o.MapFrom(s => s.RoleName))
                    .ReverseMap();
        }
    }
}
