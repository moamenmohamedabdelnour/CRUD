using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IMapper mapper, IUnitOfWork unitOfWork /*IDepartmentRepository departmentRepository*/)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            //_departmentRepository = departmentRepository;
        }
        public IActionResult Index(string SearchInp)
        {
            var employees=Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(SearchInp))
                employees = _unitOfWork.EmployeeRepository.GetAll();
            else
                employees = _unitOfWork.EmployeeRepository.GetEmployeeByName(SearchInp.ToLower());

            var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmp);

        }

        public IActionResult Details(int? id , string viewName="Details")
        {
            if(id == null)
            {
                return BadRequest();
            }
            var employees = _unitOfWork.EmployeeRepository.Get(id.Value);
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employees);
            if(employees == null)
            {
                return NotFound();
            }
            return View(viewName, mappedEmp);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"] = _departmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if(ModelState.IsValid)
            {
                employeeVM.ImageName= DocumentSettings.UploadFile(employeeVM.Image,"Images");
                var mappedEmp=_mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                 _unitOfWork.EmployeeRepository.Add(mappedEmp);
                var Count = _unitOfWork.Complete();
                if (Count > 0)
                    TempData["Message"] = "Employee Created Sucessfully";
                else
                    TempData["Message"] = "Error Creating Employee";

                return RedirectToAction("Index");
                
            }
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(employeeVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //ViewData["Departments"] = _departmentRepository.GetAll();
            return Details(id,"Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int? id,EmployeeViewModel employeeVM)
        {
            if(id != employeeVM.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmp=_mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Update(mappedEmp);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVM);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id,"Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(/*[FromRoute]int? id*/EmployeeViewModel employeeVM)
        {
   
            try
            {
            var mappedEmp=_mapper.Map<EmployeeViewModel,Employee>(employeeVM);
            _unitOfWork.EmployeeRepository.Delete(mappedEmp);
            var count = _unitOfWork.Complete();
            if (count > 0)
                    DocumentSettings.Delete(employeeVM.ImageName,"Images");
            return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVM);
            }
           

        }

    }
}
