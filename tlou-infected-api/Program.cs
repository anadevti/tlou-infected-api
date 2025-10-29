using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using tlou_infected_api.Data;
using DotNetEnv;
using MongoDB.Bson.Serialization;
using tlou_infected_api.Data.Serialization;
using tlou_infected_api.Domain.Enums;
using tlou_infected_api.Repository;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

var connectionString = builder.Configuration["MONGODB_URI"];
var client = new MongoClient(connectionString);
var database = client.GetDatabase("tlou-db");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<AppDbContext>();
builder.Services.AddSingleton<IMongoDatabase>(database);
builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
builder.Services.AddScoped<tlou_infected_api.Application.Services.SurvivorService>();

var app = builder.Build();

BsonSerializer.RegisterSerializer(typeof(InfectedStageSmartEnum), new InfectedStageSmartEnumSerializer());

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