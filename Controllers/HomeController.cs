using EmployeeManagement.Models;
 
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.ViewModels;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace EmployeeManagement.Controllers;

//[Route("[controller]")] -> it was attribute Routing in asp.net core mvc
public class HomeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IHostingEnvironment hostingEnvironment;

    public HomeController(IEmployeeRepository employeeRepository,
        IHostingEnvironment hostingEnvironment)
    {
        _employeeRepository = employeeRepository;
        this.hostingEnvironment = hostingEnvironment;
    }
    /*[Route("")]
    [Route("[action]")]
    [Route("~/")]*/
    public ViewResult Index()
    {
        var model = _employeeRepository.GetAllEmployee();
        return View(model);
    }
  // [Route("[action]/{id?}")]
    
    public ViewResult Details(int? id)
    {
        Employee employee = _employeeRepository.GetEmployee(id.Value);
        if (employee == null)
        {
            Response.StatusCode = 404;
            return View("EmplyeeNotFound",id.Value);
        }
        HomeDetailsViewModel homeDetailsviewModel = new HomeDetailsViewModel()
        {
            Employee = employee,
            PageTitle = "Employee Details"
        };
        return View(homeDetailsviewModel);
    }
    [HttpGet]
    public ViewResult Create()
    {
        return View();
    }
   [HttpGet]
    public ViewResult Edit(int id)
    {
        Employee employee = _employeeRepository.GetEmployee(id);
        EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
        {
            Id = employee.Id,
            Name = employee.Name,
            Email = employee.Email,
            Department = employee.Department,
            ExistingPhotoPath = employee.PhotoPath
        };
        return View(employeeEditViewModel);
    }
    [HttpPost]
    public IActionResult Edit(EmployeeEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            Employee employee = _employeeRepository.GetEmployee(model.Id);
            employee.Name = model.Name;
            employee.Email = model.Email;
            employee.Department = model.Department;
            if (model.Photo != null)
            {
                if (model.ExistingPhotoPath != null)
                {
                   string filePath = Path.Combine(hostingEnvironment.WebRootPath,"images", model.ExistingPhotoPath);
                   System.IO.File.Delete(filePath); 
                }
                employee.PhotoPath = ProcessUploadedFile(model);
            }
            _employeeRepository.Update(employee);
            return RedirectToAction("index" );
        }
        return View(model);
        
    }

    private string? ProcessUploadedFile(EmployeeCreateViewModel model)
    {
        string uniqueFileName = null;
        if (model.Photo != null )
        {
                
            // hostingEnvironment is the parameter of HomeController constructor
            string uploadsFolder =  Path.Combine(hostingEnvironment.WebRootPath,"images");
                
            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
            string filePath = Path.Combine( uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.Photo.CopyTo(fileStream);
            }
           
        }

        return uniqueFileName;
    }

    [HttpPost]
    public IActionResult Create(EmployeeCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            string uniqueFileName = ProcessUploadedFile(model);

            Employee newEmployee = new Employee
            {
                Name = model.Name,
                Email = model.Email,
                Department = model.Department,
                PhotoPath = uniqueFileName
            };
            _employeeRepository.Add(newEmployee);
            return RedirectToAction("details",new { id = newEmployee.Id });
        }
        return View();
        
    }
    
}