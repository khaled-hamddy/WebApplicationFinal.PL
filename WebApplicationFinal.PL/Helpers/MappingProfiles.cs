using AutoMapper;
using WebApplicationFinal.DAL.Models;
using WebApplicationFinal.PL.ViewModels;

namespace WebApplicationFinal.PL.Helpers
{
    public class MappingProfiles :Profile
    { //neccesary for instructing auto mapping how to map
        public MappingProfiles() {
            CreateMap<EmployeeViewModel,Employee>().ReverseMap();
            //CreateMap<EmployeeViewModel, Employee>().ForMember(d=>d.Name,o=>o.MapFrom(s=>s.EmpName));
            CreateMap<DepartmentViewModel,Department>().ReverseMap();
        }
    }
}
