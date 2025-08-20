using tlou_infected_api.Data;
using MongoDB.Bson.Serialization;
using tlou_infected_api.Data.Serialization;
using tlou_infected_api.Domain.Enums;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<AppDbContext>();

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