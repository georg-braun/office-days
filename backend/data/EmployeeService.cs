using office_days_api.model;

namespace office_days.data;

public class EmployeeService
{
    private readonly JsonFilePersistence _jsonFilePersistence;

    public EmployeeService(JsonFilePersistence jsonFilePersistence)
    {
        _jsonFilePersistence = jsonFilePersistence;
        Employees = _jsonFilePersistence.Load().ToList();
    }

    private List<Employee> Employees = new List<Employee>();

    public async Task AddEmployee(string name)
    {
        Employees.Add(new Employee(){Id = Guid.NewGuid(), Name = name, OfficeDays = new List<DateTime>()});
        await _jsonFilePersistence.Save(Employees);
    }
    
    public async Task DeleteEmployee(Guid id)
    {
        Employees.RemoveAll(_ => _.Id.Equals(id));
        await _jsonFilePersistence.Save(Employees);
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
}