using Bikes.Application.Contracts;
using Bikes.Application.Contracts.Bikes;
using Microsoft.AspNetCore.Mvc;

namespace Bikes.Api.Host.Controllers;

/// <summary>
/// Controller for bike management
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BikesController(IBikeService bikeService)
    : CrudControllerBase<BikeDto, BikeCreateUpdateDto>
{
    protected override IApplicationService<BikeDto, BikeCreateUpdateDto> Service => bikeService;
}