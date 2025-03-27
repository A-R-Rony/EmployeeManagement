using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData(
            new Employee
            {
                Id = 1,
                Name = "Rony",
                Department = Dept.IT,
                Email = "Rony@gmail.com"
            }
        );
    }
}