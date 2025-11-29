using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.StationResponse;

public class Station
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
}
