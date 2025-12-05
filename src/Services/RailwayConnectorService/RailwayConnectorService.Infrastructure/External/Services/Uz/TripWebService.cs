using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RailwayConnectorService.Application.Interfaces;
using RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;
using RailwayConnectorService.Infrastructure.Configuration;
using RailwayConnectorService.Infrastructure.External.Models;
using Serilog;

namespace RailwayConnectorService.Infrastructure.External.Services.Uz;

public class TripWebService : BaseWebService, ITripWebService
{
    private readonly string _baseUrl;

    public TripWebService(IHttpClientFactory httpClientFactory, ILogger logger, IOptions<UzApiOptions> options, IHttpContextAccessor httpContextAccessor)
        : base(HttpClientName.UZ, httpClientFactory, logger, httpContextAccessor)
    {
        _baseUrl = options.Value.BaseUrl;
    }

    public Task<Trip> GetTripAsync(long stationFromId, long stationToId, string date, string uzAccessToken, bool withTransfers = false)
    {
        var url = $"{_baseUrl}v3/trips?station_from_id={stationFromId}&station_to_id={stationToId}&with_transfers={(withTransfers ? 1 : 0)}&date={date}";
        return GetAsync<Trip>(url, uzAccessToken);
    }

    public Task<Direct> GetTripAsync(long tripId, string uzAccessToken)
    {
        var url = $"{_baseUrl}v3/trips/{tripId}";
        return GetAsync<Direct>(url, uzAccessToken);
    }

    public Task<List<string>> GetDepartureDatesAsync(long stationFromId, long stationToId, string uzAccessToken)
    {
        var url = $"{_baseUrl}trips/departure-dates?station_from_id={stationFromId}&station_to_id={stationToId}";
        return GetAsync<List<string>>(url, uzAccessToken);
    }

    public Task<WagonByClass> GetWagonByClassAsync(long tripId, string wagonClass, string uzAccessToken)
    {
        var encodedWagonClass = Uri.EscapeDataString(wagonClass);
        var url = $"{_baseUrl}v3/trips/{tripId}/wagons-by-class/{encodedWagonClass}";
        return GetAsync<WagonByClass>(url, uzAccessToken);
    }
}
