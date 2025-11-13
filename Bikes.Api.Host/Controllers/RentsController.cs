using Bikes.Application.Contracts;
using Bikes.Application.Contracts.Rents;
using Microsoft.AspNetCore.Mvc;

namespace Bikes.Api.Host.Controllers;

/// <summary>
/// Controller for rent management
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentsController(IRentService rentService)
    : CrudControllerBase<RentDto, RentCreateUpdateDto>
{
    protected override IApplicationService<RentDto, RentCreateUpdateDto> Service => rentService;
}