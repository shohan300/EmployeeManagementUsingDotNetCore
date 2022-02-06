using EmployeeApp.Model;
using EmployeeApp.Repositories;
using EmployeeApp.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApp.Controllers
{
    public class EmployeeController:Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private IEmployee _EmployeeRepository;
        public EmployeeController(IEmployee employeeRepository, IWebHostEnvironment hostingEnvironment)
        {
            this._EmployeeRepository = employeeRepository;
            this._hostingEnvironment = hostingEnvironment;
        }


        public IActionResult Index()
        {
            List<Employee> empList = _EmployeeRepository.GetAllEmployee().ToList();
            return View(empList);
        }
        public IActionResult Create()
        {
            ViewBag.Department = _EmployeeRepository.GetAllDepartment();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel obj)
        {
            if (ModelState.IsValid)
            {
                string unqueFileName = ProcessFileUpload(obj);
                Employee employee = new Employee
                {
                    EmployeeId = obj.EmployeeId,
                    EmployeeName = obj.EmployeeName,
                    EmployeeEmail = obj.EmployeeEmail,
                    EmployeePhone = obj.EmployeePhone,
                    Address = obj.Address,
                    DepartmentId = obj.DepartmentId,
                    PhotoPath = unqueFileName
                };
                _EmployeeRepository.SaveEmployee(employee);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Departments = _EmployeeRepository.GetAllDepartment();
                return View();
            }
        }

        private string ProcessFileUpload(EmployeeViewModel obj)
        {
            string unqueFileName = null;
            if (obj.Photo != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                unqueFileName = Guid.NewGuid().ToString() + "_" + obj.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, unqueFileName);
                using (var FileStream = new FileStream(filePath, FileMode.Create))
                {
                    obj.Photo.CopyTo(FileStream);
                }
            }
            return unqueFileName;
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Department = _EmployeeRepository.GetAllDepartment();
            Employee emp = _EmployeeRepository.GetEmployeeById(id);
            EmployeeViewModel viewModel = GetEditemployee(emp);
            return View(viewModel);
        }

        private EmployeeViewModel GetEditemployee(Employee emp)
        {
            EmployeeViewModel editemp = new EmployeeViewModel
            {
                EmployeeId = emp.EmployeeId,
                EmployeeName = emp.EmployeeName,
                EmployeeEmail = emp.EmployeeEmail,
                EmployeePhone = emp.EmployeePhone,
                Address = emp.Address,
                DepartmentId = emp.DepartmentId,
                ExistingPhotoPath = emp.PhotoPath
            };
            return editemp;
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel obj)
        {
            Employee empObj = _EmployeeRepository.GetEmployeeById(obj.EmployeeId);
            if (ModelState.IsValid)
            {
                empObj.EmployeeName = obj.EmployeeName;
                empObj.EmployeeEmail = obj.EmployeeEmail;
                empObj.EmployeePhone = obj.EmployeePhone;
                empObj.Address = obj.Address;
                empObj.DepartmentId = obj.DepartmentId;
                if (obj.Photo != null)
                {
                    empObj.PhotoPath = ProcessFileUpload(obj);
                    if (obj.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", obj.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                }
                Employee emp = _EmployeeRepository.UpdateEmployee(empObj);
                return RedirectToAction("Index");
            }
            EmployeeViewModel viewModel = GetEditemployee(empObj);
            return View(viewModel);
        }


        public IActionResult Delete(int id)
        {
            Employee pass = _EmployeeRepository.GetEmployeeById(id);
            EmployeeViewModel viewModel = GetEditemployee(pass);
            return View(viewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            Employee empObj = _EmployeeRepository.GetEmployeeById(id);
            if (empObj.PhotoPath != null)
            {
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", empObj.PhotoPath);
                System.IO.File.Delete(filePath);
                Employee emp = _EmployeeRepository.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
            EmployeeViewModel viewModel = GetEditemployee(empObj);
            return View(viewModel);
        }

        public IActionResult _Details(int id)
        {
            Employee empObj = _EmployeeRepository.GetEmployeeById(id);
            return View(empObj);
        }
    }
}
