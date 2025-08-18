using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using tlou_infected_api.Domain.Enums;

namespace tlou_infected_api.Domain.Entities;

public class Infected
{
    [BsonId]
    [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("_type"), BsonRepresentation(BsonType.ObjectId)]
    public InfectedStageSmartEnum Type { get; set; }
    [BsonElement("_description"), BsonRepresentation(BsonType.String)]
    public string Description { get; set; }
    [BsonElement("_image"), BsonRepresentation(BsonType.String)]
    public string Image { get; set; }
    [BsonElement("_weaknesses"), BsonRepresentation(BsonType.String)]
    public string Weaknesses { get; set; }
}