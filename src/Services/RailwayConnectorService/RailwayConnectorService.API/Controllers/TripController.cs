using Microsoft.AspNetCore.Mvc;
using RailwayConnectorService.Application.Services.Interfaces;

namespace RailwayConnectorService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet("{tripId}")]
    public async Task<IActionResult> GetTrip(int tripId)
    {
        var trip = await _tripService.GetTripAsync(tripId, "");
        if (trip == null)
        {
            return NotFound();
        }

        return Ok(trip);
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips([FromQuery] int stationFromId,
                                              [FromQuery] int stationToId,
                                              [FromQuery] string date,
                                              [FromQuery] bool withTransfers = false)
    {
        var trips = await _tripService.GetTripAsync(stationFromId, stationToId, date, "", withTransfers);
        if (trips == null)
        {
            return NotFound();
        }

        return Ok(trips);
    }

    [HttpGet("departure-dates")]
    public async Task<IActionResult> GetDepartureDates([FromQuery] int stationFromId,
                                                       [FromQuery] int stationToId)
    {
        var dates = await _tripService.GetDepartureDatesAsync(stationFromId, stationToId, "");
        return Ok(dates);
    }
}