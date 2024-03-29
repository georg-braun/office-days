using office_days_api.model;

namespace office_days.data;

public class EmployeeService
{
    private readonly JsonFilePersistence<Employee> _jsonFilePersistence;

    public EmployeeService(IConfiguration configuration)
    {
        
        _jsonFilePersistence = new JsonFilePersistence<Employee>(configuration, "employees.json");
        Employees = _jsonFilePersistence.Load().ToList();
    }

    private List<Employee> Employees = new List<Employee>();

    public Task AddEmployee(string name)
    {
        Employees.Add(new Employee(){Id = Guid.NewGuid(), Name = name, OfficeDays = new List<DateTime>()});
        return Task.CompletedTask;
    }
    
    public Task DeleteEmployee(Guid id)
    {
        Employees.RemoveAll(_ => _.Id.Equals(id));
        return Task.CompletedTask;
    }

    public IEnumerable<Employee> GetAll()
    {
        return Employees.ToList();
    }

    public void SetAtOffice(Guid id, DateTime date)
    {
        var employee = Employees.Find(_ => _.Id.Equals(id));
        if (employee is null) return;
        if (!employee.OfficeDays.Contains(date))
            employee.OfficeDays.Add(date);
    }
    
    public void SetAtHome(Guid id, DateTime date)
    {
        var employee = Employees.Find(_ => _.Id.Equals(id));
        if (employee is null) return;
        employee.OfficeDays.Remove(date);
    }

    public async Task Save()
    {
        await _jsonFilePersistence.Save(Employees);
    }
}