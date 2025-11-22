using RailwayConnectorService.Application.Interfaces;
using RailwayConnectorService.Application.Services.Interfaces;
using RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

namespace RailwayConnectorService.Application.Services;

public class TripService : ITripService
{
    private readonly ITripWebService _tripWebService;

    public TripService(ITripWebService tripWebService)
    {
        _tripWebService = tripWebService;
    }

    public async Task<List<Direct>> GetTripAsync(int stationFromId, int stationToId, string date, bool withTransfers = false)
    {
        return await _tripWebService.GetTripsAsync(stationFromId, stationToId, date, withTransfers);
    }

    public async Task<Direct> GetTripAsync(int tripId)
    {
        return await _tripWebService.GetTripAsync(tripId);
    }

    public async Task<List<string>> GetDepartureDatesAsync(int stationFromId, int stationToId)
    {
        return await _tripWebService.GetDepartureDatesAsync(stationFromId, stationToId);
    }
}
