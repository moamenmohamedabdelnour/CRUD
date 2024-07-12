﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Employee :ModelBase
    {
        public string Name { get; set; }


        public int Age { get; set; }
        public string Address { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }


        public string Email { get; set; }


        public string PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }

        public string ImageName { get; set; }
        public int? DepartmentId { get; set; }//Forign key column
        //Navigational Property
        //[InverseProperty(nameof(Models.Department.Employees))]
        public Department Department { get; set; }

    }
}
