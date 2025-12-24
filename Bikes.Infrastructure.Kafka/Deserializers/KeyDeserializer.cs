using Confluent.Kafka;
using System.Text;

namespace Bikes.Infrastructure.Kafka.Deserializers;

public class KeyDeserializer : IDeserializer<Guid>
{
    public Guid Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if (isNull || data.Length == 0)
            return Guid.Empty;

        var guidString = Encoding.UTF8.GetString(data);
        return Guid.Parse(guidString);
    }
}