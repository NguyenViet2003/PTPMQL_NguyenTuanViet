using Microsoft.AspNetCore.Mvc;
using CSDL.Models;
using System.Collections.Generic;
using System.Linq;

namespace CSDL.Models.Controllers
{
    public class EmployeeController : Controller
    {
        // Giả định có một danh sách nhân viên để thao tác
        private static List<Employee> employees = new List<Employee>();

        // Action hiển thị danh sách nhân viên
        public IActionResult Index()
        {
            return View(employees);
        }

        // Action hiển thị form tạo mới nhân viên
        public IActionResult Create()
        {
            return View();
        }

        // Action xử lý việc tạo mới nhân viên
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employees.Add(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // Action hiển thị chi tiết nhân viên
        public IActionResult Details(string id)
        {
            var employee = employees.FirstOrDefault(e => e.PersonId == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // Action hiển thị form chỉnh sửa nhân viên
        public IActionResult Edit(string id)
        {
            var employee = employees.FirstOrDefault(e => e.PersonId == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // Action xử lý việc chỉnh sửa nhân viên
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var existingEmployee = employees.FirstOrDefault(e => e.PersonId == employee.PersonId);
                if (existingEmployee == null)
                {
                    return NotFound();
                }
                existingEmployee.FullName = employee.FullName;
                existingEmployee.Address = employee.Address;
                existingEmployee.PhoneNumber = employee.PhoneNumber;
                existingEmployee.Age = employee.Age;
                existingEmployee.EmployeeId = employee.EmployeeId;
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // Action xóa nhân viên
        public IActionResult Delete(string id)
        {
            var employee = employees.FirstOrDefault(e => e.PersonId == id);
            if (employee == null)
            {
                return NotFound();
            }
            employees.Remove(employee);
            return RedirectToAction("Index");
        }
    }
}