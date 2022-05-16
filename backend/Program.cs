using Microsoft.AspNetCore.Mvc;
using office_days.data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<JsonFilePersistence>();
builder.Services.AddSingleton<EmployeeService>();
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


app.MapGet("/", () =>  "Hello");
app.MapGet("/get-all-employees", (EmployeeService employeeService) =>  employeeService.GetAll());
app.MapPost("/add-employee", (EmployeeService employeeService, string name) =>  employeeService.AddEmployee(name));
app.MapPost("/set-at-office", (EmployeeService employeeService, Guid id, DateTime date) =>  employeeService.SetAtOffice(id, date));
app.MapPost("/set-at-home", (EmployeeService employeeService, Guid id, DateTime date) =>  employeeService.SetAtHome(id, date));
app.MapGet("/delete-employee", (EmployeeService employeeService, Guid id) =>  employeeService.DeleteEmployee(id));

app.Run();