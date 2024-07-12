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
	public class DepartmentRepository : GenaricRepository<Department> , IDepartmentRepository
	{
        //private readonly AppDbContext _dbContext;

        public DepartmentRepository(AppDbContext dbContext):base(dbContext)
        {
            //_dbContext = dbContext;  //et3amalo initialize f el base class (Genaric Repo)
        }
        //private readonly AppDbContext _dbcontext; //Null
        //      public DepartmentRepository(AppDbContext dbContext)
        //      {
        //          _dbcontext = dbContext;
        //      }
        //      public int Add(Department entity)
        //{
        //	_dbcontext.Departments.Add(entity);
        //	return _dbcontext.SaveChanges();
        //}

        //public int Delete(Department entity)
        //{
        //	_dbcontext.Departments.Remove(entity);
        //	return _dbcontext.SaveChanges();
        //}

        //public Department Get(int id)
        //{
        //	return _dbcontext.Departments.Find(id); //de btadwar f el memeory w lw mal2tosh hatro7 tgebo mn el datbase
        //	//return _dbcontext.Find<Department>(id);

        //}

        //public IEnumerable<Department> GetAll()
        //{
        //	return _dbcontext.Departments;
        //}


        //public int Update(Department entity)
        //{
        //	_dbcontext.Departments.Update(entity);
        //	return _dbcontext.SaveChanges();
        //}
    }
}
