using RailwayConnectorService.Application.Interfaces;
using RailwayConnectorService.Application.Services.Interfaces;
using RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

namespace RailwayConnectorService.Application.Services;

public class TripService : ITripService
{
    private readonly ITripWebService _tripWebService;
    private readonly ICacheService _cacheService;

    public TripService(ITripWebService tripWebService, ICacheService cacheService)
    {
        _tripWebService = tripWebService;
        _cacheService = cacheService;
    }

    public async Task<Trip> GetTripAsync(long stationFromId, long stationToId, string date, string uzAccessToken, bool withTransfers = false)
    {
        var cacheKey = $"trip:search:{stationFromId}:{stationToId}:{date}:{withTransfers}";
        var cached = await _cacheService.GetAsync<Trip>(cacheKey);
        if (cached != null)
        {
            return cached;
        }

        var response = await _tripWebService.GetTripAsync(stationFromId, stationToId, date, uzAccessToken, withTransfers);
        await _cacheService.SetAsync(cacheKey, response, TimeSpan.FromSeconds(30));
        return response;
    }

    public async Task<Direct> GetTripAsync(long tripId, string uzAccessToken)
    {
        var cacheKey = $"trip:direct:{tripId}";
        var cached = await _cacheService.GetAsync<Direct>(cacheKey);
        if (cached != null)
        {
            return cached;
        }

        var response = await _tripWebService.GetTripAsync(tripId, uzAccessToken);
        await _cacheService.SetAsync(cacheKey, response, TimeSpan.FromSeconds(30));
        return response;
    }

    public async Task<DepartureDate> GetDepartureDatesAsync(long stationFromId, long stationToId, string uzAccessToken)
    {
        var cacheKey = $"trip:dates:{stationFromId}:{stationToId}";
        var cached = await _cacheService.GetAsync<DepartureDate>(cacheKey);
        if (cached != null)
        {
            return cached;
        }

        var response = await _tripWebService.GetDepartureDatesAsync(stationFromId, stationToId, uzAccessToken);
        var departureDate = new DepartureDate
        {
            Dates = response
        };
        await _cacheService.SetAsync(cacheKey, departureDate, TimeSpan.FromSeconds(30));
        return departureDate;
    }

    public async Task<WagonByClass> GetWagonByClassAsync(long tripId, string wagonClass, string uzAccessToken)
    {
        var cacheKey = $"trip:wagon:{tripId}:{wagonClass}";
        var cached = await _cacheService.GetAsync<WagonByClass>(cacheKey);
        if (cached != null)
        {
            return cached;
        }

        var response = await _tripWebService.GetWagonByClassAsync(tripId, wagonClass, uzAccessToken);
        await _cacheService.SetAsync(cacheKey, response, TimeSpan.FromSeconds(30));
        return response;
    }
}
