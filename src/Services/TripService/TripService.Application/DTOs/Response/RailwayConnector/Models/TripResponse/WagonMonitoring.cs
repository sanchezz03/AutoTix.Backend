using Newtonsoft.Json;

namespace TripService.Application.DTOs.Response.RailwayConnector.Models.TripResponse;

public class WagonMonitoring
{
    [JsonProperty("available_type")]
    public string AvailableType { get; set; }

    [JsonProperty("active")]
    public object Active { get; set; }
}
