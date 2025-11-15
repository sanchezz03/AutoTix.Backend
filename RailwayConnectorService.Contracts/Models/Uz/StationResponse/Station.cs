using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.StationResponse;

public class Station
{
    [JsonProperty("id")]
    public int Id;

    [JsonProperty("name")]
    public string Name;
}
