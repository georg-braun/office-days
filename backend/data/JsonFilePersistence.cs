using System.Text.Json;
using office_days_api.model;

namespace office_days.data;



public class JsonFilePersistence<T>
{
    private string dataFolder;
    private readonly string filePath;

  
    public JsonFilePersistence(IConfiguration configuration, string fileName)
    {
        dataFolder = configuration["DataFolder"].Replace("\"", "");
        Console.WriteLine($"Data-Folder: ${dataFolder}");
        filePath = $"{dataFolder}{fileName}";
        Console.Write($"FilePath: {filePath}");
        if (!File.Exists(filePath))
        {
            Console.WriteLine("create json data file");
            var stream = File.Create(filePath);
            stream.Close();
        }
    }

    public async Task Save(IEnumerable<T> employees)
    {
        try
        {
            Console.WriteLine($"Save file to {filePath}");
            await using var createStream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(createStream, employees);
            await createStream.DisposeAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public IEnumerable<T> Load()
    {
        var content = File.ReadAllText(filePath);
        if (string.IsNullOrEmpty(content))
            return Array.Empty<T>();
        var result = JsonSerializer.Deserialize<T[]>(content);
        return result ?? Array.Empty<T>();
    }
}