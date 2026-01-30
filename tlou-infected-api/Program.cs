using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using tlou_infected_api.Data;
using MongoDB.Bson.Serialization;
using tlou_infected_api.Application.Services;
using tlou_infected_api.Data.Serialization;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Domain.Enums;
using tlou_infected_api.Handlers;
using tlou_infected_api.Repository;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

var connectionString = builder.Configuration["MONGODB_URI"] ?? builder.Configuration["MongoDB:ConnectionString"];
var client = new MongoClient(connectionString);
var database = client.GetDatabase("tlou-db");

// Add services to the container.
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Configure JSON serialization BEFORE building the app
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new BsonDocumentJsonConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<AppDbContext>();
builder.Services.AddSingleton<IMongoDatabase>(database);
builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IMongoRepository<InventorySurvivor>, MongoRepository<InventorySurvivor>>();
builder.Services.AddScoped<tlou_infected_api.Application.Services.SurvivorService>();
builder.Services.AddScoped<InventoryService>();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

BsonSerializer.RegisterSerializer(typeof(InfectedStageSmartEnum), new InfectedStageSmartEnumSerializer());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();