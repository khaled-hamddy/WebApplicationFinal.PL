using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationFinal.DAL.Models
{
    public class Department: ModelBase
    {
      
        public string code { get; set; }
       
        public string name { get; set; }
      
        public DateTime DateOfCreation { get; set; }
        //[InverseProperty(nameof(Employee.Department))]
        public ICollection<Employee> Employees { get; set; }=new HashSet<Employee>();
    }
}
