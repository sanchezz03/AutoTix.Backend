using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.TripResponse;

public class Hint
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("button_text")]
    public string ButtonText { get; set; }
}
