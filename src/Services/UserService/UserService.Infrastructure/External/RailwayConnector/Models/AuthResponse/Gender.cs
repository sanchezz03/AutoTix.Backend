using Newtonsoft.Json;

namespace UserService.Infrastructure.External.RailwayConnector.Models.AuthResponse;

public class Gender
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }
}
