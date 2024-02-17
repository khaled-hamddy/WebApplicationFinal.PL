using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplicationFinal.BLL.Interfaces;
using WebApplicationFinal.DAL.Models;
using WebApplicationFinal.PL.Helpers;
using WebApplicationFinal.PL.ViewModels;

namespace WebApplicationFinal.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IEmployeeRepository _employeeRepo;
        //private readonly IDepartmentRepository _departmentRepo;

        public EmployeeController(IMapper mapper,IUnitOfWork unitOfWork/* IEmployeeRepository employeeRepo, IDepartmentRepository departmentRepo*/)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            //_employeeRepo = employeeRepo;
            //_departmentRepo = departmentRepo; /*inject it in view for better performance because index doesnt use it*/

        }
        public IActionResult Index(string searchInp)
        {
            #region Comment
            //TempData.Keep();
            //ViewData["Message"] = "Hello ViewData";
            //ViewBag.Message = "Hello ViewBag"; 
            #endregion
            var employees = Enumerable.Empty<Employee>();
            if (String.IsNullOrEmpty(searchInp))
             employees = _unitOfWork.EmployeeRepository.GetAll();
            else
                employees= _unitOfWork.EmployeeRepository.SearchByName(searchInp.ToLower());
            var mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return View(mappedEmps);
        }
        [HttpGet] //to go to empty form
        public IActionResult Create()
        {
            //ViewData["Departments"] = _departmentRepo.GetAll();

            //ViewBag.Departments = _departmentRepo.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {

            if (ModelState.IsValid) //server side validation for model validations
            {
                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "images");

                var mappedEmp =_mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                 _unitOfWork.EmployeeRepository.Add(mappedEmp);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    TempData["Message"] = "Department is Created Successfully";
                    
                }
                else
                {
                    TempData["Message"] = "An Error Has Ocurred, Department Not Created :(";

                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }
        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue) // nullable has two properties hasvalue and value
                return BadRequest();
            var employee = _unitOfWork.EmployeeRepository.Get(id.Value); // should use id.value as it accepts parameter in int form
            if (employee is null)
                return NotFound();
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

          
            return View(viewName, mappedEmp);
        }
        // httpget by default
        public IActionResult Edit(int? id)
        {
            //ViewData["Departments"]=_departmentRepo.GetAll();
            //ViewBag.Departments = _departmentRepo.GetAll();
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to refuse any edit on say id as example from resources otherthan website
        public IActionResult Edit([FromRoute]/*to make it take id only from url to avoid any other way to change it*/int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                //try and catch because it may throw exception if this Employee is in relation in database and it is immutable

                try
                {
                    employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "images");

                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                    _unitOfWork.EmployeeRepository.Update(mappedEmp);
                    _unitOfWork.Complete();  
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    //Log Exception
                    //Friendly Message
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
            }
            return View(employeeVM);
        }
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to refuse any edit on say id as example from resources otherthan website
        public IActionResult Delete([FromRoute]/*to make it take id only from url to avoid any other way to change it*/int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();

            //try and catch because it may throw exception if this Employee is inrelation in database and it is immutable
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitOfWork.EmployeeRepository.Delete(mappedEmp);
               var count= _unitOfWork.Complete();
                if(count > 0)
                    DocumentSettings.DeleteFile(employeeVM.ImageName, "images");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //Log Exception
                //Friendly Message
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVM);

            }

        }

    }
}
