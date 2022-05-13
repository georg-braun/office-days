namespace office_days_api.model;

public class Employee
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<DateTime> OfficeDays { get; set; }
}