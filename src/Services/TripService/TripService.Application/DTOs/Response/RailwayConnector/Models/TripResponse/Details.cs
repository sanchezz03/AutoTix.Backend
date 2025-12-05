using Newtonsoft.Json;

namespace TripService.Application.DTOs.Response.RailwayConnector.Models.TripResponse;

public class Details
{
    [JsonProperty("photo")]
    public string Photo { get; set; }

    [JsonProperty("content")]
    public List<Content> Content { get; set; }
}
