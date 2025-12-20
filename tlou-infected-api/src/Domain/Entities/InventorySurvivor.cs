using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tlou_infected_api.Domain.Entities;

public class InventorySurvivor
{
    [BsonId]
    [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("_medical_kit"), BsonRepresentation(BsonType.String)]
    public string? MedicalKit { get; set; }
    
    [BsonElement("_brick"), BsonRepresentation(BsonType.String)]
    public string? Brick { get; set; }
    
    [BsonElement("_knife"), BsonRepresentation(BsonType.String)]
    public string? Knife { get; set; }
    
    [BsonElement("_flame_thrower"), BsonRepresentation(BsonType.String)]
    public string? Flamethrower { get; set; }
    
    [BsonElement("_pills"), BsonRepresentation(BsonType.String)]
    public string? Pills { get; set; }
    
    [BsonElement("_is_active"), BsonRepresentation(BsonType.Boolean)]
    public bool? IsActive { get; set; }
}