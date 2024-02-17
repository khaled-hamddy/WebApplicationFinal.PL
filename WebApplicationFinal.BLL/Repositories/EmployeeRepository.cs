using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationFinal.BLL.Interfaces;
using WebApplicationFinal.DAL.Data;
using WebApplicationFinal.DAL.Models;

namespace WebApplicationFinal.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository //we here implemented iemployee because we have there methods specific to employee and common methods in genericrepo
    {
        //private readonly AppDbContext _dbContext; inherited from generic

        public EmployeeRepository(AppDbContext dbContext):base(dbContext) /*we make dependency injection and pass it to base because if we leave it parameterless there will be error because there is no parameterless constructor in Generic Repo TO CHAIN ON IT*/ 
        {
        }
        public IQueryable<Employee> GetEmployeeByAddress(string address)
        {
            return _dbContext.Employees.Where(e => e.Address.ToLower().Contains(address.ToLower()));
        }
        public IQueryable<Employee> SearchByName(string name)
            => _dbContext.Employees.Where(E=>E.Name.ToLower().Contains(name));

        
    }
}
