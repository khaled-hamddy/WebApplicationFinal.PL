using AutoMapper;
using WebApplicationFinal.DAL.Models;
using WebApplicationFinal.PL.ViewModels;

namespace WebApplicationFinal.PL.Helpers
{
    public class UserProfile:Profile
    {
      public UserProfile() 
        {
        CreateMap<ApplicationUser,UserViewModel>().ReverseMap();
        }
    }
}
