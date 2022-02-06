using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApp.Model
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }


        [Required, MaxLength(50)]
        public string EmployeeName { get; set; }


        [Required, MaxLength(50)]
        public string EmployeeEmail { get; set; }


        [Required, MaxLength(50)]
        public string EmployeePhone { get; set; }


        [Required]
        public string Address { get; set; }

        public string PhotoPath { get; set; }

        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}
