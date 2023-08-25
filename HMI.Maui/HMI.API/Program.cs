using HMI.API.Data;
using HMI.API.DTO;
using HMI.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HMIContext>(options =>
       options.UseSqlite("DataSource=.\\DB\\HMI.db"));
builder.Services.AddScoped<DbService>();    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapPost("/events", async ([FromBody] EventsRequest request, DbService dbService) =>
    {
    var events = await dbService.GetEventsAsync(request);
    return events;
}).WithName("GetEvents")
    .WithOpenApi();

app.Run();


