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
        HomeDetailsViewModel homeDetailsviewModel = new HomeDetailsViewModel()
        {
            Employee = _employeeRepository.GetEmployee(id ?? 1),
            PageTitle = "Employee Details"
        };
        return View(homeDetailsviewModel);
    }
    [HttpGet]
    public ViewResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(EmployeeCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                // hostingEnvironment is the parameter of HomeController constructor
                string uploadsFolder =  Path.Combine(hostingEnvironment.WebRootPath,"images");
                
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine( uploadsFolder, uniqueFileName);
                model.Photo.CopyTo(new FileStream(filePath, FileMode.Create) );
            }

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