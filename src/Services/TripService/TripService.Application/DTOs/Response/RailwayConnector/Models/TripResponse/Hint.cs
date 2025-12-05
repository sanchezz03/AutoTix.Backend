using Newtonsoft.Json;

namespace TripService.Application.DTOs.Response.RailwayConnector.Models.TripResponse;
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
