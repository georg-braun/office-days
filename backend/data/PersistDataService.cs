using System.Globalization;

namespace office_days.data;

public class PersistDataService : IHostedService, IDisposable
{
    private readonly RoomService _roomService;
    private readonly EmployeeService _employeeService;

    public PersistDataService(EmployeeService employeeService, RoomService roomService)
    {
        _roomService = roomService;
        _employeeService = employeeService;
        Console.WriteLine("Starting PersistDataService background task.");
    }

    private Timer _timer = null;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    private async void DoWork(object state)
    {
        await _employeeService.Save();
        await _roomService.Save();
    }
    
    public void Dispose()
    {
        _timer?.Dispose();
    }
}