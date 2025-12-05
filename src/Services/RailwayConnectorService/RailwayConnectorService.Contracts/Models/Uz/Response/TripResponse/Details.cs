using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

public class Details
{
    [JsonProperty("photo")]
    public string Photo { get; set; }

    [JsonProperty("content")]
    public List<Content> Content { get; set; }
}
