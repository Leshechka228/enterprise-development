using Confluent.Kafka;
using System.Text;
using System.Text.Json;
using Bikes.Application.Contracts.Rents;

namespace Bikes.Infrastructure.Kafka.Deserializers;

public class ValueDeserializer : IDeserializer<IList<RentCreateUpdateDto>>
{
    public IList<RentCreateUpdateDto> Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if (isNull || data.Length == 0)
            return [];

        var json = Encoding.UTF8.GetString(data);

        try
        {
            var result = JsonSerializer.Deserialize<IList<RentCreateUpdateDto>>(json);
            return result ?? [];
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException($"Failed to deserialize JSON: {json}", ex);
        }
    }
}