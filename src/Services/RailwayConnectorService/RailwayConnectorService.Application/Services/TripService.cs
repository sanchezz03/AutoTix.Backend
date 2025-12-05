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

    public async Task<Trip> GetTripAsync(long stationFromId, long stationToId, string date, string uzAccessToken, bool withTransfers = false)
    {
        return await _tripWebService.GetTripAsync(stationFromId, stationToId, date, uzAccessToken, withTransfers);
    }

    public async Task<Direct> GetTripAsync(long tripId, string uzAccessToken)
    {
        return await _tripWebService.GetTripAsync(tripId, uzAccessToken);
    }

    public async Task<List<string>> GetDepartureDatesAsync(long stationFromId, long stationToId, string uzAccessToken)
    {
        return await _tripWebService.GetDepartureDatesAsync(stationFromId, stationToId, uzAccessToken);
    }

    public async Task<WagonByClass> GetWagonByClassAsync(long tripId, string wagonClass, string uzAccessToken)
    {
        return await _tripWebService.GetWagonByClassAsync(tripId, wagonClass, uzAccessToken);
    }
}
