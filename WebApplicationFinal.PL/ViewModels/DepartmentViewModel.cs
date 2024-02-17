using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using WebApplicationFinal.DAL.Models;

namespace WebApplicationFinal.PL.ViewModels
{
    public class DepartmentViewModel
    { 
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is Required!!")]
        public string code { get; set; }
        [Required(ErrorMessage = "Name is Required!!")]
        public string name { get; set; }
        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }
        //[InverseProperty(nameof(Employee.Department))]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
