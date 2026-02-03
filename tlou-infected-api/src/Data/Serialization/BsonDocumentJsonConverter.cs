using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Bson;

namespace tlou_infected_api.Data.Serialization;

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