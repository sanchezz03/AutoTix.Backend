using RailwayConnectorService.Application.Interfaces;
using RailwayConnectorService.Application.Services.Interfaces;
using RailwayConnectorService.Contracts.Models.Uz;
using RailwayConnectorService.Contracts.Models.Uz.TripResponse;

namespace RailwayConnectorService.Application.Services;

public class TripService : ITripService
{
    private readonly ITripWebService _tripWebService;

    public TripService(ITripWebService tripWebService)
    {
        _tripWebService = tripWebService;
    }

    public async Task<UzResponse<List<Direct>>> GetTripAsync(int stationFromId, int stationToId, string date, bool withTransfers = false)
    {
        return await _tripWebService.GetTripsAsync(stationFromId, stationToId, date, withTransfers);
    }

    public async Task<UzResponse<Direct>> GetTripAsync(int tripId)
    {
        return await _tripWebService.GetTripAsync(tripId);
    }

    public async Task<UzResponse<List<string>>> GetDepartureDatesAsync(int stationFromId, int stationToId)
    {
        return await _tripWebService.GetDepartureDatesAsync(stationFromId, stationToId);
    }
}
