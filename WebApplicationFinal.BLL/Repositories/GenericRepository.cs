using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationFinal.BLL.Interfaces;
using WebApplicationFinal.DAL.Data;
using WebApplicationFinal.DAL.Models;

namespace WebApplicationFinal.BLL.Repositories
{
    public class GenericRepository <T>: IGenericRepository<T> where T : ModelBase
    {
        private protected readonly AppDbContext _dbContext; //TO make employee and dept inherit it
        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(T entity)
        
            //_dbContext.Set<T>().Add(entity);
          =>  _dbContext.Add(entity); //E.F Core 3.1 feature 
         /*   return _dbContext.SaveChanges()*///return number of rows changed
        
        public void Update(T entity)
         => _dbContext.Update(entity);
         
        public void Delete(T entity)
        =>  _dbContext.Remove(entity);
        

        public T Get(int id)
        {
            //find search local first at memory(to check if this id is searched before ) then if it is not in memory get it from database
            return _dbContext.Find<T>(id);
        }

        public IEnumerable<T> GetAll()
        { 
        if(typeof(T) == typeof(Employee))
                return (IEnumerable<T>) _dbContext.Employees.Include(E=>E.Department).AsNoTracking().ToList();
         else
               return _dbContext.Set<T>().AsNoTracking().ToList();

        }


    }
}
