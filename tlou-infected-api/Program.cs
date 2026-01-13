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
using tlou_infected_api.Repository;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

var connectionString = builder.Configuration["MONGODB_URI"];
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
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new BsonDocumentJsonConverter());
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new BsonDocumentJsonConverter());
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

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

// TODO: arrumar um jeito de tirar isso daqui e fazer uma conversao dentro das melhores praticas.
public class BsonDocumentJsonConverter : JsonConverter<BsonDocument>
{
    public override BsonDocument Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, BsonDocument value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (var element in value)
        {
            writer.WritePropertyName(element.Name);
            WriteBsonValue(writer, element.Value);
        }

        writer.WriteEndObject();
    }

    private static void WriteBsonValue(Utf8JsonWriter writer, BsonValue bsonValue)
    {
        switch (bsonValue.BsonType)
        {
            case BsonType.ObjectId:
                writer.WriteStringValue(bsonValue.AsObjectId.ToString());
                break;
            case BsonType.String:
                writer.WriteStringValue(bsonValue.AsString);
                break;
            case BsonType.Int32:
                writer.WriteNumberValue(bsonValue.AsInt32);
                break;
            case BsonType.Int64:
                writer.WriteNumberValue(bsonValue.AsInt64);
                break;
            case BsonType.Double:
                writer.WriteNumberValue(bsonValue.AsDouble);
                break;
            case BsonType.Boolean:
                writer.WriteBooleanValue(bsonValue.AsBoolean);
                break;
            case BsonType.DateTime:
                writer.WriteStringValue(bsonValue.ToUniversalTime());
                break;
            case BsonType.Null:
                writer.WriteNullValue();
                break;
            case BsonType.Array:
                writer.WriteStartArray();
                foreach (var item in bsonValue.AsBsonArray)
                {
                    WriteBsonValue(writer, item);
                }
                writer.WriteEndArray();
                break;
            case BsonType.Document:
                writer.WriteStartObject();
                foreach (var element in bsonValue.AsBsonDocument)
                {
                    writer.WritePropertyName(element.Name);
                    WriteBsonValue(writer, element.Value);
                }
                writer.WriteEndObject();
                break;
            default:
                writer.WriteStringValue(bsonValue.ToString());
                break;
        }
    }
}
