
namespace office_days_api.model;

public class Room
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Table> Tables { get; set; }
}