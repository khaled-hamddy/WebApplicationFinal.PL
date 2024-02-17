using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationFinal.BLL.Interfaces;
using WebApplicationFinal.DAL.Models;
using WebApplicationFinal.PL.ViewModels;

namespace WebApplicationFinal.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(/*IDepartmentRepository departmentRepo*/IUnitOfWork unitOfWork,IMapper mapper) {
            _unitOfWork = unitOfWork;
            //_departmentRepo = departmentRepo;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var departments= _unitOfWork.DepartmentRepository.GetAll();
            var mappedDep = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(mappedDep);
        }
        [HttpGet] //to go to empty form
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if(ModelState.IsValid) //server side validation for model validations
            {
                var mappedDep=_mapper.Map<DepartmentViewModel,Department>(departmentVM);
                _unitOfWork.DepartmentRepository.Add(mappedDep);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(departmentVM);
        }
        [HttpGet]
        public IActionResult Details(int ?id,string viewName="Details") {
            if (!id.HasValue) // nullable has two properties hasvalue and value
                return BadRequest();
            var department=_unitOfWork.DepartmentRepository.Get(id.Value); // should use id.value as it accepts parameter in int form
            var mappedDep = _mapper.Map<Department, DepartmentViewModel>(department);
            if(department is null)
                return NotFound();
            return View(viewName,mappedDep);
        }
        // httpget by default
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }
        [HttpPost]
         [ValidateAntiForgeryToken] //to refuse any edit on say id as example from resources otherthan website
        public IActionResult Edit([FromRoute]/*to make it take id only from url to avoid any other way to change it*/int id ,DepartmentViewModel departmentVM)
        {
            if(id != departmentVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                //try and catch because it may throw exception if this department is inrelation in database and it is immutable
                try
                {
                    var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    _unitOfWork.DepartmentRepository.Update(mappedDep);
                    _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex) 
                {
                    //Log Exception
                    //Friendly Message
                    ModelState.AddModelError(string.Empty,ex.Message);   

                }
            } return View(departmentVM);
        }
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to refuse any edit on say id as example from resources otherthan website
        public IActionResult Delete([FromRoute]/*to make it take id only from url to avoid any other way to change it*/int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();
            
                //try and catch because it may throw exception if this department is inrelation in database and it is immutable
                try
                {
                var mappedDep=_mapper.Map<DepartmentViewModel,Department>(departmentVM);
                    _unitOfWork.DepartmentRepository.Delete(mappedDep);
                _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    //Log Exception
                    //Friendly Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(departmentVM);

                } 
            
        }

    }
}
