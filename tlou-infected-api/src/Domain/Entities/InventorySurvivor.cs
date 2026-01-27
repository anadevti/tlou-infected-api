using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tlou_infected_api.Domain.Entities;

public class InventorySurvivor
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("_survivor_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? SurvivorId { get; set; }

    [BsonElement("_medical_kit")]
    public string? MedicalKit { get; set; }

    [BsonElement("_brick")]
    public string? Brick { get; set; }

    [BsonElement("_knife")]
    public string? Knife { get; set; }

    [BsonElement("_flame_thrower")]
    public string? Flamethrower { get; set; }

    [BsonElement("_pills")]
    public string? Pills { get; set; }

    [BsonElement("_is_active")]
    public bool IsActive { get; set; }
}