using Newtonsoft.Json;

namespace TripService.Application.DTOs.Response.RailwayConnector.Models.StationResponse;

public class Station
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
}
