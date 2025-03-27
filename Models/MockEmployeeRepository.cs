namespace EmployeeManagement.Models;

public class MockEmployeeRepository : IEmployeeRepository
{
    private List<Employee> _employeeList;

    public MockEmployeeRepository()
    {
        _employeeList = new List<Employee>()
        {
            new Employee() { Id = 1, Name = "Rony", Department = Dept.IT, Email = "rony@gmail.com" },
            new Employee() { Id = 2, Name = "bhai", Department = Dept.HR, Email = "bhai@gmail.com" }
        };
    }
    
    public IEnumerable<Employee> GetAllEmployee()
    {
        return _employeeList;
    }

    public Employee Add(Employee employee)
    {
        employee.Id = _employeeList.Max(e=> e.Id) + 1;
        _employeeList.Add(employee);
        return employee;
    }

    public Employee Update(Employee employeeChanges)
    {
        Employee employee = _employeeList.FirstOrDefault(e => e.Id == employeeChanges.Id);
        if (employee != null)
        {
            employee.Name = employeeChanges.Name;
            employee.Department = employeeChanges.Department;
            employee.Email = employeeChanges.Email;
        }
        return employee;
    }

    public Employee Delete(int id)
    {
        Employee employee = _employeeList.FirstOrDefault(e => e.Id == id);
        if (employee != null)
        {
            _employeeList.Remove(employee);
        }
        return employee;
    }

    public Employee GetEmployee(int Id)
    {
        return _employeeList.FirstOrDefault(e => e.Id == Id);
    }

    
}