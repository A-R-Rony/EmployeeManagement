namespace EmployeeManagement.Models;

public interface IEmployeeRepository
{
    Employee GetEmployee(int Id);
    IEnumerable<Employee> GetAllEmployee();
    // IEnumerable often used in MVC applications when passing lists of data
    // from a controller to a view.
    Employee Add(Employee employee);
    Employee Update(Employee employeeChanges);
    Employee Delete(int id);

}