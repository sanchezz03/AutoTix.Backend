using Microsoft.AspNetCore.Mvc;
using TripService.Application.Services.Interfaces;

namespace TripService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StationController : ControllerBase
{
    private readonly IStationService _tripService;

    public StationController(IStationService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips()
    {
        var trips = await _tripService.GetTripsAsync();
        return Ok(trips);
    }
}
