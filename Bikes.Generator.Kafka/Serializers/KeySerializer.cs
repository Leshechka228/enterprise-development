using Confluent.Kafka;
using System.Text;

namespace Bikes.Generator.Kafka.Serializers;

/// <summary>
/// Serializer for Guid keys in Kafka messages
/// </summary>
public class KeySerializer : ISerializer<Guid>
{
    public byte[] Serialize(Guid data, SerializationContext context)
    {
        return Encoding.UTF8.GetBytes(data.ToString());
    }
}