using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.AuthResponse;

public class Privilege
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("input_type")]
    public int InputType { get; set; }

    [JsonProperty("active")]
    public bool Active { get; set; }

    [JsonProperty("companion_id")]
    public object CompanionId { get; set; }

    [JsonProperty("rules")]
    public string Rules { get; set; }

    [JsonProperty("hint")]
    public object Hint { get; set; }
}
