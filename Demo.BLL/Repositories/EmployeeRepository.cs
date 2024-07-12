using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenaricRepository<Employee>, IEmployeeRepository
    {
        //private readonly AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext dbContext):base(dbContext)
        {
            
        }
        public IQueryable<Employee> Address(string Address)
        {
            return _dbContext.Employees.Where(E => E.Address.ToLower().Contains(Address.ToLower()));
        }

        public IQueryable<Employee> GetEmployeeByName(string Name)
        {
            return _dbContext.Employees.Where(E=>E.Name.ToLower().Contains(Name));
        }
    }
}
