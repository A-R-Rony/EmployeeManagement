using System.ComponentModel.DataAnnotations;
using EmployeeManagement.Models;

namespace EmployeeManagement.ViewModels;

public class EmployeeCreateViewModel
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-z0-9-]+\.[a-zA-Z0-9-.]+$")]
    public string Email { get; set; }
    [Required]
    public Dept? Department { get; set; }
    public List<IFormFile> Photos { get; set; }
}