using System.Text.Json;
using office_days_api.model;

namespace office_days.data;

public class JsonFilePersistence
{
    private string fileName = "employees.json";
    private string dataFolder;
    private string filePath;

    public JsonFilePersistence(IConfiguration configuration)
    {
        dataFolder = configuration["DataFolder"].Replace("\"", "");
        Console.WriteLine($"Data-Folder: ${dataFolder}");
        filePath = $"{dataFolder}employees.json";
        Console.Write($"FilePath: {filePath}");
        if (!File.Exists(filePath))
        {
            Console.WriteLine("create json data file");
            var stream = File.Create(filePath);
            stream.Close();
        }
    }

    public async Task Save(IEnumerable<Employee> employees)
    {
        await using var createStream = File.Create(filePath);
        await JsonSerializer.SerializeAsync(createStream, employees);
        await createStream.DisposeAsync();
    }

    public IEnumerable<Employee> Load()
    {
        var content = File.ReadAllText(filePath);
        if (string.IsNullOrEmpty(content))
            return Array.Empty<Employee>();
        var result = JsonSerializer.Deserialize<Employee[]>(content);
        return result ?? Array.Empty<Employee>();
    }
}