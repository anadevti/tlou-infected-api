using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using tlou_infected_api.Data;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<AppDbContext>();

var app = builder.Build();

// env vars
DotNetEnv.Env.Load();

// Carrega config + variáveis de ambiente
builder.Configuration.AddEnvironmentVariables();
var connectionString = builder.Configuration["MONGODB_URI"];
var client = new MongoClient(connectionString);
var database = client.GetDatabase("test");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// app.UseAuthorization();
app.MapControllers();
app.Run();