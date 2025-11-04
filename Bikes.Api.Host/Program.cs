using Bikes.Application.Services;
using Bikes.Application.Contracts.Analytics;
using Bikes.Application.Contracts.Bikes;
using Bikes.Application.Contracts.Models;
using Bikes.Application.Contracts.Renters;
using Bikes.Application.Contracts.Rents;
using Bikes.Domain.Repositories;
using Bikes.Infrastructure.InMemory.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services with interfaces
builder.Services.AddSingleton<IBikeRepository, InMemoryBikeRepository>();
builder.Services.AddSingleton<IBikeService, BikeService>();
builder.Services.AddSingleton<IBikeModelService, BikeModelService>();
builder.Services.AddSingleton<IRenterService, RenterService>();
builder.Services.AddSingleton<IRentService, RentService>();
builder.Services.AddSingleton<IAnalyticsService, AnalyticsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();