using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [MinLength(5, ErrorMessage = "Minimum Length of Name is 5 Chars")]
        [MaxLength(50, ErrorMessage = "Maximum Length of Name is 50 Chars")]
        public string Name { get; set; }
        public int Age { get; set; }
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
            , ErrorMessage = "Address Must Be Like 123-Street-City-Country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
