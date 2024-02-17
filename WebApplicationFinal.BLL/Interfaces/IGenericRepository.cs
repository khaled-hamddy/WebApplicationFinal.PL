using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationFinal.DAL.Models;

namespace WebApplicationFinal.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : ModelBase /*we make model base for this point sepcific to make it doesnt accept any type other than our models*/
    {
        IEnumerable<T> GetAll();
        T Get(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
