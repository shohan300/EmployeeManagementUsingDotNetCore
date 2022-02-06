using EmployeeApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApp.Repositories
{
    public class EmployeeRepositorie : IEmployee
    {
        private readonly EmployeeDbContext _context;
        public EmployeeRepositorie(EmployeeDbContext context)
        {
            this._context = context;

        }

        public Employee DeleteEmployee(int id)
        {
            Employee emp = GetEmployeeById(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                _context.SaveChanges();
            }
            return emp;
        }

        public IEnumerable<Department> GetAllDepartment()
        {
            return _context.Departments;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            var emp = _context.Employees.Include(e => e.Department).ToList();
            return emp;
            //return _context.Employees;
        }

        public Employee GetEmployeeById(int id)
        {
            Employee emp = _context.Employees.Find(id);
            return emp;
        }

        public Department SaveDept(Department obj)
        {
            _context.Departments.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public Employee SaveEmployee(Employee obj)
        {
            _context.Employees.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public Employee UpdateEmployee(Employee upobj)
        {
            var emp = _context.Employees.Attach(upobj);
            emp.State = EntityState.Modified;
            _context.SaveChanges();
            return upobj;
        }
    }
}
