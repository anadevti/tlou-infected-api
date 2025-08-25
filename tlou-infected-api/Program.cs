using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using tlou_infected_api.Data;
using DotNetEnv;
using MongoDB.Bson.Serialization;
using tlou_infected_api.Data.Serialization;
using tlou_infected_api.Domain.Enums;
using tlou_infected_api.Repository;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["MONGODB_URI"];
var client = new MongoClient(connectionString);
var database = client.GetDatabase("test");
// Add services to the container.
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<AppDbContext>();
builder.Services.AddSingleton<IMongoDatabase>(database);
builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

var app = builder.Build();

DotNetEnv.Env.Load();

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