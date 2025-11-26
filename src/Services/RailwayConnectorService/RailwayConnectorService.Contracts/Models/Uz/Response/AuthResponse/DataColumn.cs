using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.AuthResponse;

public class DataColumn
{
    [JsonProperty("value")]
    public string Value { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }
}
