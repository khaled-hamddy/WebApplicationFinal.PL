using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationFinal.DAL.Models;

namespace WebApplicationFinal.BLL.Interfaces
{
    public interface IDepartmentRepository :IGenericRepository<Department>
    {
        //method specific for departments+ +methods of Igeneric 
    }
}
