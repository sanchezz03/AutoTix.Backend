using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

public class WagonMonitoring
{
    [JsonProperty("available_type")]
    public string AvailableType { get; set; }

    [JsonProperty("active")]
    public object Active { get; set; }
}
