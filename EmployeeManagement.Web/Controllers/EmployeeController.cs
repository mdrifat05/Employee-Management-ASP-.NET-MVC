﻿using EmployeeManagement.Service.Contracts;
using EmployeeManagement.Service.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: Employee
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var employees = await _employeeService.Get(cancellationToken);
            return View(employees);
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.Get(id, cancellationToken);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeDto createEmployeeDto, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                bool result = await _employeeService.Create(createEmployeeDto, cancellationToken);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(createEmployeeDto);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.Get(id, cancellationToken);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateEmployeeDto updateEmployeeDto, CancellationToken cancellationToken)
        {
            if (id != updateEmployeeDto.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                bool result = await _employeeService.Update(updateEmployeeDto, cancellationToken);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(updateEmployeeDto);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.Get(id, cancellationToken);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            bool result = await _employeeService.Delete(id, cancellationToken);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
