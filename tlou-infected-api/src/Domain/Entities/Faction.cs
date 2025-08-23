using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using tlou_infected_api.Domain.Enums;

namespace tlou_infected_api.Domain.Entities;


public class Faction
{
    [BsonId]
    [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
    
    public string? Id { get; set; }
    
    [BsonElement("_faction_type"), BsonRepresentation(BsonType.String)]
    public string FactionType { get; set; }
    
    [BsonElement("__faction_status"), BsonRepresentation(BsonType.Boolean)]
    public bool FactionStatus { get; set; }
    
    [BsonElement("_territory"), BsonRepresentation(BsonType.String)]
    public string Territory { get; set; }
}