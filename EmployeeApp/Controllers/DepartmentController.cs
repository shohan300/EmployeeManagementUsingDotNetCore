using EmployeeApp.Model;
using EmployeeApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApp.Controllers
{
    public class DepartmentController: Controller
    {
        private IEmployee _employeeRepository;
        public DepartmentController(IEmployee employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {

            return View(_employeeRepository.GetAllDepartment());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department obj)
        {
            if (ModelState.IsValid)
            {
                Department train = new Department
                {
                    DepartmentName = obj.DepartmentName,
                };
                _employeeRepository.SaveDept(train);
            }
            return RedirectToAction("Index", _employeeRepository.GetAllDepartment());
        }
    }
}
