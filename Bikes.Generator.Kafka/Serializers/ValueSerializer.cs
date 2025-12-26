using Confluent.Kafka;
using System.Text;
using System.Text.Json;
using Bikes.Application.Contracts.Rents;

namespace Bikes.Generator.Kafka.Serializers;

/// <summary>
/// Serializer for list of RentCreateUpdateDto values in Kafka messages
/// </summary>
public class ValueSerializer : ISerializer<IList<RentCreateUpdateDto>>
{
    public byte[] Serialize(IList<RentCreateUpdateDto> data, SerializationContext context)
    {
        if (data == null || data.Count == 0)
            return [];

        var json = JsonSerializer.Serialize(data);
        return Encoding.UTF8.GetBytes(json);
    }
}