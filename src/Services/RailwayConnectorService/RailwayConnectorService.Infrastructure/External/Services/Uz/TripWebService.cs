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

    public TripWebService(IHttpClientFactory httpClientFactory, ILogger logger,
        IHttpContextAccessor httpContextAccessor, IOptions<UzApiOptions> options)
        : base(HttpClientName.UZ, httpClientFactory, logger, httpContextAccessor, options)
    {
        _baseUrl = options.Value.BaseUrl;
    }

    public Task<List<Direct>> GetTripsAsync(int stationFromId, int stationToId, string date, bool withTransfers = false)
    {
        var url = $"{_baseUrl}trips?station_from_id={stationFromId}&station_to_id={stationToId}&with_transfers={(withTransfers ? 1 : 0)}&date={date}";
        return GetAsync<List<Direct>>(url);
    }

    public Task<Direct> GetTripAsync(int tripId)
    {
        var url = $"{_baseUrl}trips/{tripId}";
        return GetAsync<Direct>(url);
    }

    public Task<List<string>> GetDepartureDatesAsync(int stationFromId, int stationToId)
    {
        var url = $"{_baseUrl}trips/departure-dates?station_from_id={stationFromId}&station_to_id={stationToId}";
        return GetAsync<List<string>>(url);
    }
}
