using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationFinal.BLL.Interfaces;
using WebApplicationFinal.BLL.Repositories;
using WebApplicationFinal.DAL.Data;

namespace WebApplicationFinal.BLL
{
    public class UnitOfWork :IUnitOfWork
    { //responsible for db context role to be accesed by presentation layer to make presentation layer able to access db context
        private readonly AppDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository { get; set; } // like dbset in db context
        public IDepartmentRepository DepartmentRepository { get; set; }
        public UnitOfWork(AppDbContext dbContext) 
        {
           _dbContext = dbContext;
            EmployeeRepository=new EmployeeRepository(_dbContext);// intialization to not refer to null
            DepartmentRepository=new DepartmentRepository(_dbContext);
        }
        public int Complete() //like save changes in db context
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose() //without it coonection with database will be (still)  open  and to be done auto make iunitofwork inerit idispose
        {
            _dbContext.Dispose();
        }

    }
}
