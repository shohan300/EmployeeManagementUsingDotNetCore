using EmployeeApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApp.Repositories
{
    public interface IEmployee
    {
        IEnumerable<Employee> GetAllEmployee();
        Employee GetEmployeeById(int id);
        Employee SaveEmployee(Employee obj);
        Employee UpdateEmployee(Employee upobj);
        Employee DeleteEmployee(int id);
        Department SaveDept(Department obj);
        IEnumerable<Department> GetAllDepartment();
    }
}
