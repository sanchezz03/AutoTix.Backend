using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

public class Content
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }
}
