using Newtonsoft.Json;

namespace TripService.Application.DTOs.Response.RailwayConnector.Models.StationResponse;

public class Departure
{
    [JsonProperty("train")]
    public string Train { get; set; }

    [JsonProperty("route")]
    public string Route { get; set; }

    [JsonProperty("time")]
    public int Time { get; set; }

    [JsonProperty("platform")]
    public string Platform { get; set; }
}
