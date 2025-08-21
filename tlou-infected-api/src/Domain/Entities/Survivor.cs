using System.ComponentModel.DataAnnotations;
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
    
    
   [BsonElement("_life"), BsonRepresentation(BsonType.Int32)]
   [Range(1,10)]
   public int Life { get; set; }
    
    [BsonElement("_strength"), BsonRepresentation(BsonType.Int32)]
    [Range(1,5)]
    public int Strength { get; set; }
    
    [BsonElement("_agility"), BsonRepresentation(BsonType.Int32)]
    [Range(1,5)]
    public int Agility { get; set; }
    
    [BsonElement("_mainWeapon"), BsonRepresentation(BsonType.String)]
    
    public string MainWeapon { get; set; }
    
    [BsonElement("_stealth"), BsonRepresentation(BsonType.Int32)]
    [Range(1,5)]
    
    public int Stealth { get; set; }
    
}