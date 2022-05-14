using System.Globalization;

namespace office_days.data;

public class PersistDataService : IHostedService, IDisposable
{
    public EmployeeService EmployeeService { get; }

    public PersistDataService(EmployeeService employeeService)
    {
        EmployeeService = employeeService;
        Console.WriteLine("Starting PersistDataService background task.");
    }

    private Timer _timer = null;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    private async void DoWork(object state)
    {
        await EmployeeService.Save();
    }
    
    public void Dispose()
    {
        _timer?.Dispose();
    }
}