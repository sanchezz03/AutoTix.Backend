using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.StationResponse;

public class Station
{
    [JsonProperty("id")]
    public int Id;

    [JsonProperty("name")]
    public string Name;
}
