using Bikes.Application.Contracts.Analytics;
using Bikes.Application.Contracts.Bikes;
using Bikes.Application.Contracts.Models;
using Bikes.Application.Contracts.Renters;
using Bikes.Application.Contracts.Rents;
using Bikes.Application.Services;
using Bikes.Domain.Repositories;
using Bikes.Infrastructure.EfCore;
using Bikes.ServiceDefaults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddDbContext<BikesDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("bikes-db"));
    options.EnableSensitiveDataLogging();
    options.LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddScoped<IBikeRepository, EfCoreBikeRepository>();
builder.Services.AddScoped<IBikeModelRepository, EfCoreBikeModelRepository>();
builder.Services.AddScoped<IRenterRepository, EfCoreRenterRepository>();
builder.Services.AddScoped<IRentRepository, EfCoreRentRepository>();

builder.Services.AddScoped<IBikeService, BikeService>();
builder.Services.AddScoped<IBikeModelService, BikeModelService>();
builder.Services.AddScoped<IRenterService, RenterService>();
builder.Services.AddScoped<IRentService, RentService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapDefaultEndpoints();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BikesDbContext>();
    context.Database.Migrate();
    context.Seed();
}

app.Run();