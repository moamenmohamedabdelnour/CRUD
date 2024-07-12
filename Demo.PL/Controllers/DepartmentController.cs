using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Demo.PL.Controllers
{
	[Authorize]
	public class DepartmentController : Controller
	{
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartmentRepository _departmentsRepo;
        public DepartmentController(IMapper mapper, IUnitOfWork unitOfWork/*IDepartmentRepository departmentsRepo*/) //Ask CLR to craete object from class implemnting IDepartmentRepository
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            //_departmentsRepo = departmentsRepo;
        }
        public IActionResult Index()
		{
			var department = _unitOfWork.DepartmentRepository.GetAll();
            var mappedDep = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(department);
            return View(mappedDep);
		}

		
		//da ely haiwadini 3la saf7t el create
		[HttpGet]	
		public IActionResult Create()
		{
			return View();
		}



		//w da ely hai3ml submit ll create
		[HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if(ModelState.IsValid)
			{
				var mappedDep=_mapper.Map<DepartmentViewModel,Department>(departmentVM);
				 _unitOfWork.DepartmentRepository.Add(mappedDep);
				var Count = _unitOfWork.Complete();
				if (Count > 0)
					return RedirectToAction("Index");
			}
			return View(departmentVM);
        }

		
		public IActionResult Details(int? id,string viewName ="Details")
		{
			
			if(id is null)
			{
				return BadRequest();
			}
			var department = _unitOfWork.DepartmentRepository.Get(id.Value);
            var mappedDep = _mapper.Map<Department, DepartmentViewModel>(department);
            if (department is null)
			{
				return NotFound();
			}
			return View(viewName, mappedDep);
		}
		//   /Departmen/Edit
		//   /Department/Edit/10
		[HttpGet]
		public IActionResult Edit(int? id)
		{
			return Details(id,"Edit");
			//if(id is null)
			//{
			//	return BadRequest();
			//}
			//var department = _departmentsRepo.Get(id.Value);
			//if(department is null)
			//{
			//	return NotFound();
			//}
			//return View(department);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit([FromRoute]int? Id,DepartmentViewModel departmentVM)
		{
			if (departmentVM.Id!=Id)
				return BadRequest();
				try
				{
                    var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
					_unitOfWork.DepartmentRepository.Update(mappedDep);
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
					return View(departmentVM);
				}

		}

		public IActionResult Delete(int? id)
		{
			return Details(id, "Delete");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? Id,DepartmentViewModel departmentVM)
        {
            if (Id != departmentVM.Id)
                return BadRequest();
            
                try
                {
					var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
					_unitOfWork.DepartmentRepository.Delete(mappedDep);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
					return View(departmentVM);
				}
            
        }
    }
}
