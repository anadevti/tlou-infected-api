using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using tlou_infected_api.Application.Services;
using tlou_infected_api.Data;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Repository;

[MemoryDiagnoser] // coletar e exibir métricas de uso de memória (alocação, coleta de lixo, etc.)
[InProcess]
[HtmlExporter]
public class MyBenchmarks
{
    private ServiceProvider _serviceProvider;
    private InventoryService _inventoryService;

    [GlobalSetup]
    public void SetupInventory()
    {
        var services = new ServiceCollection();

        var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI") ?? "mongodb://localhost:27017";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("tlou-db");

        services.AddSingleton<AppDbContext>();
        services.AddSingleton<IMongoDatabase>(database);
        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IMongoRepository<InventorySurvivor>, MongoRepository<InventorySurvivor>>();
        services.AddScoped<SurvivorService>();
        services.AddScoped<InventoryService>();

        _serviceProvider = services.BuildServiceProvider();
        _inventoryService = _serviceProvider.GetRequiredService<InventoryService>();
    }

    [Benchmark]
    public async Task<int> TestInventoryUpdate()
    {
        var inventory = new InventorySurvivor { Id = "bench-id" };
        var result = await _inventoryService.UpdateInventorySurvivors(inventory);
        return result is ICollection<InventorySurvivor> c ? c.Count : System.Linq.Enumerable.Count(result);
    }
}