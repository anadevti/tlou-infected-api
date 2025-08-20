using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using tlou_infected_api.Domain.Enums;

namespace tlou_infected_api.Data.Serialization;

public class InfectedStageSmartEnumSerializer : SerializerBase<InfectedStageSmartEnum>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, InfectedStageSmartEnum value)
    {
        context.Writer.WriteString(value.Name);
    }

    public override InfectedStageSmartEnum Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var name = context.Reader.ReadString();
        return InfectedStageSmartEnum.FromName(name);
    }
}