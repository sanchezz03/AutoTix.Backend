using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.AuthResponse;

public class Points
{
    [JsonProperty("value")]
    public string Value { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }
}
