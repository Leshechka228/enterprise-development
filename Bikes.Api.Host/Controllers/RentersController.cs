using Bikes.Application.Contracts;
using Bikes.Application.Contracts.Renters;
using Microsoft.AspNetCore.Mvc;

namespace Bikes.Api.Host.Controllers;

/// <summary>
/// Controller for renter management
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentersController(IRenterService renterService)
    : CrudControllerBase<RenterDto, RenterCreateUpdateDto>
{
    protected override IApplicationService<RenterDto, RenterCreateUpdateDto> Service => renterService;
}