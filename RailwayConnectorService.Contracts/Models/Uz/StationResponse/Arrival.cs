using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.StationResponse;

public class Arrival
{
    [JsonProperty("train")]
    public string Train { get; set; }

    [JsonProperty("route")]
    public string Route { get; set; }

    [JsonProperty("time")]
    public int Time { get; set; }

    [JsonProperty("platform")]
    public string Platform { get; set; }

    [JsonProperty("delay_minutes")]
    public int? DelayMinutes { get; set; }
}
