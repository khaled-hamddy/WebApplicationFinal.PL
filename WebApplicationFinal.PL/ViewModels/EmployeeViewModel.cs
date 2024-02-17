using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using WebApplicationFinal.DAL.Models;

namespace WebApplicationFinal.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50, ErrorMessage = "Max Length of Name is 50 Chars")]
        [MinLength(5, ErrorMessage = "Min Length of Name is 5 Chars")]
        public string Name { get; set; }
        [Range(22, 30)]
        public int? Age { get; set; }
        //[RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a,zA-Z]{5-10}$",
        //    ErrorMessage = "Address must be like 123-Street-City-Country")]
        [RegularExpression(@"^\d{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
        ErrorMessage = "Address must be in the format 123-Street-City-Country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateTime HireDate { get; set; }

       public  IFormFile  Image { get; set; } //for putting image on server

        public string ? ImageName { get; set; } //for mapping to employee imagename column in employee model
        public int? DepartmentId { get; set; }
        //[InverseProperty(nameof(Models.Department.Employees))]
        public Department? Department { get; set; }
    }
}
