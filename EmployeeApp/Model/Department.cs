using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApp.Model
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }


        [Required, MaxLength(50)]
        public string DepartmentName { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
