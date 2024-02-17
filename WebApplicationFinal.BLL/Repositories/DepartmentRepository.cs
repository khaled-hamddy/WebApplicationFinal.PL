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
    public class DepartmentRepository : GenericRepository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext dbContext):base(dbContext)
        {
        
        }
      
    }
}
