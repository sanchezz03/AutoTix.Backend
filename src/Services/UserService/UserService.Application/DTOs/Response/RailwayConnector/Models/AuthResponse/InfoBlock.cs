using Newtonsoft.Json;

namespace UserService.Application.DTOs.Response.RailwayConnector.Models.AuthResponse;

public class InfoBlock
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }
}
