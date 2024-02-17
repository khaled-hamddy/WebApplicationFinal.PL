using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationFinal.DAL.Models;

namespace WebApplicationFinal.BLL.Interfaces
{
    public interface IEmployeeRepository :IGenericRepository<Employee>
    {
        //method specific for Employees+ +methods of Igeneric 
        // iused iqueryable instead of ienumerable because iqueryable make filtration in database while ienumerable do it in memory so iqueryable is better
        IQueryable<Employee> GetEmployeeByAddress(string address);

        IQueryable<Employee> SearchByName(string name);
    }
}
