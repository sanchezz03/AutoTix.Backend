using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.AuthResponse;

public class AdditionalAction
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("icon")]
    public string Icon { get; set; }

    [JsonProperty("link")]
    public string Link { get; set; }
}
