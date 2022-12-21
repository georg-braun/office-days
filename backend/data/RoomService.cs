using office_days_api.model;

namespace office_days.data;

public class RoomService
{
    private readonly JsonFilePersistence<Room> _jsonFilePersistence;

    public RoomService(IConfiguration configuration)
    {
        
        _jsonFilePersistence = new JsonFilePersistence<Room>(configuration, "rooms.json");
        Rooms = _jsonFilePersistence.Load().ToList();
    }

    private List<Room> Rooms = new List<Room>();

    public Task AddRoom(string name)
    {
        Rooms.Add(new Room(){Id = Guid.NewGuid(), Name = name, Tables = new List<Table>()});
        return Task.CompletedTask;
    }
    
    public Task AddTable(Guid roomId, string name)
    {
        var room = Rooms.Find(_ => _.Id.Equals(roomId));
        if (room is null) return Task.CompletedTask;
        
        room.Tables.Add(Table.Init(name));
        return Task.CompletedTask;
    }
    
    public Task DeleteRoom(Guid id)
    {
        Rooms.RemoveAll(_ => _.Id.Equals(id));
        return Task.CompletedTask;
    }
    
    public Task DeleteTable(Guid roomId, Guid tableId)
    {
        var room = Rooms.Find(_ => _.Id.Equals(roomId));

        room?.Tables.RemoveAll(_ => _.Id.Equals(tableId));
        return Task.CompletedTask;
    }

    public IEnumerable<Room> GetAll()
    {
        return Rooms.ToList();
    }

    public void ToggleReservation(Guid roomId, Guid tableId, DateTime date, string person)
    {
        var room = Rooms.Find(_ => _.Id.Equals(roomId));
        if (room is null) return;

        var table = room.Tables.Find(_ => _.Id.Equals(tableId));
        if (table is null) return;

        if (table.ReservedDates.Contains(date))
            table.ReservedDates.Remove(date);
        else
            table.ReservedDates.Add((date));
    }
  

    public async Task Save()
    {
        await _jsonFilePersistence.Save(Rooms);
    }

    public void AddImageToRoom(Guid roomId, string imageBase64)
    {
        var room = Rooms.Find(_ => _.Id.Equals(roomId));
        room.Image = imageBase64;
    }
}