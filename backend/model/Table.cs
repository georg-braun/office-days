namespace office_days_api.model;

public class Table
{
    public Guid Id { get; set; }
    public string name { get; set; }
    public List<DateTime> ReservedDates { get; set; }

    public static Table Init(string name)
    {
        var table = new Table();
        table.Id = Guid.NewGuid();
        table.name = name;
        table.ReservedDates = new List<DateTime>();

        return table;
    }
}