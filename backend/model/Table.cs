namespace office_days_api.model;


public class Reservation
{
    public DateTime Date { get; set; }
    
    public string Who { get; set; }
}
public class Table
{
    public Guid Id { get; set; }
    public string name { get; set; }
    public List<Reservation> Reservations { get; set; }

    public static Table Init(string name)
    {
        var table = new Table();
        table.Id = Guid.NewGuid();
        table.name = name;
        table.Reservations = new List<Reservation>();

        return table;
    }
}