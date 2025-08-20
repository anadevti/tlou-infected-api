using Microsoft.Extensions.WebEncoders.Testing;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using tlou_infected_api.Domain.Enums;


namespace tlou_infected_api.Domain.Entities;

public class Survivor
{
    [BsonId]
    [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
    
    public string? Id { get; set; }
    
    [BsonElement("_life"), BsonRepresentation(BsonType.ObjectId)]
    
    public string Life { get; set; }
    
    [BsonElement("_strength"), BsonRepresentation(BsonType.ObjectId)]
    
    public string Strength { get; set; }
    
    [BsonElement("_agility"), BsonRepresentation(BsonType.ObjectId)]
    
    public string Agility { get; set; }
    
    [BsonElement("_mainWeapon"), BsonRepresentation(BsonType.ObjectId)]
    
    public string MainWeapon { get; set; }
    
    [BsonElement("_stealth"), BsonRepresentation(BsonType.ObjectId)]
    
    public string Stealth { get; set; }
    
}