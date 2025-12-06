using Microsoft.AspNetCore.Mvc;
using TripService.Application.Services.Interfaces;

namespace TripService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripController : ControllerBase
{
    private readonly IRouteService _routeService;

    public TripController(IRouteService routeService)
    {
        _routeService = routeService;
    }

    [HttpGet("{tripId}")]
    public async Task<IActionResult> GetTrip(long tripId)
    {
        var trip = await _routeService.GetTripAsync(tripId);
        if (trip == null)
            return NotFound();

        return Ok(trip);
    }

    [HttpGet]
    public async Task<IActionResult> GetTrip([FromQuery] long stationFromId,
                                             [FromQuery] long stationToId,
                                             [FromQuery] string date,
                                             [FromQuery] bool withTransfers = false)
    {
        var trips = await _routeService.GetTripAsync(stationFromId, stationToId, date, withTransfers);
        if (trips == null)
            return NotFound();

        return Ok(trips);
    }

    [HttpGet("departure-dates")]
    public async Task<IActionResult> GetDepartureDates([FromQuery] long stationFromId,
                                                       [FromQuery] long stationToId)
    {
        var dates = await _routeService.GetDepartureDateAsync(stationFromId, stationToId);
        return Ok(dates);
    }

    [HttpGet("{tripId}/wagons-by-class/{wagonClass}")]
    public async Task<IActionResult> GetWagonsByClass(long tripId, string wagonClass)
    {
        var result = await _routeService.GetWagonByClassAsync(tripId, wagonClass);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
