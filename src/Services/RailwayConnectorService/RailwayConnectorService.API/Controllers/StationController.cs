using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayConnectorService.Application.Services.Interfaces;

namespace RailwayConnectorService.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class StationController : ControllerBase
{
    private readonly IStationService _stationService;

    public StationController(IStationService stationService)
    {
        _stationService = stationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetStations()
    {
        var result = await _stationService.GetStationsAsync();
        return Ok(result);
    }

    [HttpGet("boards")]
    public async Task<IActionResult> GetStationBoards()
    {
        var result = await _stationService.GetStationBoardsAsync();
        return Ok(result);
    }

    [HttpGet("boards/{id}")]
    public async Task<IActionResult> GetStationBoard(long id)
    {
        var result = await _stationService.GetStationBoardAsync(id);
        return Ok(result);
    }
}
