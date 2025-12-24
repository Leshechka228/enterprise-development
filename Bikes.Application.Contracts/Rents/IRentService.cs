namespace Bikes.Application.Contracts.Rents;

/// <summary>
/// Service for rent management
/// </summary>
public interface IRentService : IApplicationService<RentDto, RentCreateUpdateDto>
{
    /// <summary>
    /// Receive and process list of rent contracts from Kafka
    /// </summary>
    public Task ReceiveContract(IList<RentCreateUpdateDto> contracts);
}