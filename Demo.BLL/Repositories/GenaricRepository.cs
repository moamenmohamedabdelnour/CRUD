using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : ModelBase
    {

        private protected readonly AppDbContext _dbContext;

        public GenaricRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            //return _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            //return _dbContext.SaveChanges();
        }

        public T Get(int id)
        {
            return _dbContext.Set<T>().Find(id); 
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))  
                return(IEnumerable<T>) _dbContext.Employees.Include(E=>E.Department).AsNoTracking().ToList();
             else
                return _dbContext.Set<T>().AsNoTracking().ToList();
        }

        public void Update(T entity)
        {

            _dbContext.Set<T>().Update(entity);
            //return _dbContext.SaveChanges();
        }
    }
}
