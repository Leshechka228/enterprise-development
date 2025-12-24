using Bikes.Infrastructure.Kafka;
using Bikes.Infrastructure.Kafka.Deserializers;
using Bikes.ServiceDefaults;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddSingleton<KeyDeserializer>();
builder.Services.AddSingleton<ValueDeserializer>();

builder.Services.AddDbContext<Bikes.Infrastructure.EfCore.BikesDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("bikes-db")
        ?? "Host=localhost;Port=5432;Database=bikes;Username=postgres;Password=postgres";
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<Bikes.Domain.Repositories.IRentRepository, Bikes.Infrastructure.EfCore.EfCoreRentRepository>();
builder.Services.AddScoped<Bikes.Domain.Repositories.IBikeRepository, Bikes.Infrastructure.EfCore.EfCoreBikeRepository>();
builder.Services.AddScoped<Bikes.Domain.Repositories.IRenterRepository, Bikes.Infrastructure.EfCore.EfCoreRenterRepository>();

builder.Services.AddScoped<Bikes.Application.Services.RentService>();
builder.Services.AddScoped<Bikes.Application.Contracts.Rents.IRentService>(sp =>
    sp.GetRequiredService<Bikes.Application.Services.RentService>());

builder.Services.AddHostedService<KafkaConsumer>();

var host = builder.Build();
await host.RunAsync();