using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApp.ViewModel
{
    public class EmployeeViewModel
    {
        [Display(Name = "Employee Id")]
        [Key]
        public int EmployeeId { get; set; }


        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter Employee Name")]
        [DataType(DataType.Text)]
        public string EmployeeName { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter Employee Email")]
        [DataType(DataType.EmailAddress)]
        public string EmployeeEmail { get; set; }


        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Enter Employee Phone")]
        public string EmployeePhone { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Enter Address")]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Select Department")]
        public int DepartmentId { get; set; }

        [Display(Name = "Department")]

        public string DepartmentName { get; set; }


        [Display(Name = "Image")]

        public string PhotoPath { get; set; }
        public IFormFile Photo { get; set; }

        public string ExistingPhotoPath { get; set; }
    }
}
