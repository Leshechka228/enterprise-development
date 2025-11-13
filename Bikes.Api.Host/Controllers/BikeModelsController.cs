using Bikes.Application.Contracts;
using Bikes.Application.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bikes.Api.Host.Controllers;

/// <summary>
/// Controller for bike model management
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BikeModelsController(IBikeModelService bikeModelService)
    : CrudControllerBase<BikeModelDto, BikeModelCreateUpdateDto>
{
    protected override IApplicationService<BikeModelDto, BikeModelCreateUpdateDto> Service => bikeModelService;
}