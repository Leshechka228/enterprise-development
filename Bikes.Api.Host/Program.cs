using Bikes.Application.Services;
using Bikes.Application.Contracts.Analytics;
using Bikes.Application.Contracts.Bikes;
using Bikes.Application.Contracts.Models;
using Bikes.Application.Contracts.Renters;
using Bikes.Application.Contracts.Rents;
using Bikes.Domain.Repositories;
using Bikes.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext with PostgreSQL
builder.Services.AddDbContext<BikesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services - ВСЕ сервисы делаем Scoped
builder.Services.AddScoped<IBikeRepository, EfCoreBikeRepository>();
builder.Services.AddScoped<IBikeService, BikeService>();
builder.Services.AddScoped<IBikeModelService, BikeModelService>();
builder.Services.AddScoped<IRenterService, RenterService>();
builder.Services.AddScoped<IRentService, RentService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

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

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BikesDbContext>();
    context.Seed();
}

app.Run();