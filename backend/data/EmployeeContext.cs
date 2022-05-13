using Microsoft.EntityFrameworkCore;
using office_days_api.model;

namespace office_days.data;

public class EmployeeContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    
    public string DbPath { get; set; }
    
    public EmployeeContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "employees.db");
        Console.WriteLine($"Put database in {DbPath}");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}