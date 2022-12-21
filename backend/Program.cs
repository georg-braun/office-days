using Microsoft.AspNetCore.Mvc;
using office_days.data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<EmployeeService>();
builder.Services.AddSingleton<RoomService>();
builder.Services.AddCors();
builder.Services.AddHostedService<PersistDataService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();



app.MapGet("/", () =>  "Hello :)");
app.MapGet("/get-all-employees", (EmployeeService employeeService) =>  employeeService.GetAll());
app.MapPost("/add-employee", (EmployeeService employeeService, string name) =>  employeeService.AddEmployee(name));
app.MapPost("/set-at-office", (EmployeeService employeeService, Guid id, DateTime date) =>  employeeService.SetAtOffice(id, date));
app.MapPost("/set-at-home", (EmployeeService employeeService, Guid id, DateTime date) =>  employeeService.SetAtHome(id, date));
app.MapGet("/delete-employee", (EmployeeService employeeService, Guid id) =>  employeeService.DeleteEmployee(id));

// Rooms
app.MapGet("/room-utilization", (RoomService roomService) => roomService.GetAll());
app.MapGet("/delete-room", (RoomService roomService, Guid roomId) =>  roomService.DeleteRoom(roomId));
app.MapGet("/delete-table", (RoomService roomService, Guid roomId, Guid tableId) =>  roomService.DeleteTable(roomId, tableId));

app.MapPost("/add-room", (RoomService roomService, string name) => roomService.AddRoom(name));
app.MapPost("/add-table", (RoomService roomService, Guid roomId, string name) => roomService.AddTable(roomId, name));
app.MapPost("/toggle-reservation", (RoomService roomService, Guid roomId, Guid tableId, DateTime date, string person) => roomService.ToggleReservation(roomId, tableId, date, person));


app.MapPost("/update-room-image", async (Guid roomId, [FromBody] string image, RoomService roomService) =>
{
    // image should be base64 encoded.
    const int maxFileSizeInBytes = 300 * 1024;

    if (image.Length > maxFileSizeInBytes)
        return Results.Problem($"The image size is to big. Max allowed {maxFileSizeInBytes} Bytes.");

    roomService.AddImageToRoom(roomId, image);

    Console.WriteLine($"Image changed for room {roomId}");

    return Results.Ok();

});

app.Run();