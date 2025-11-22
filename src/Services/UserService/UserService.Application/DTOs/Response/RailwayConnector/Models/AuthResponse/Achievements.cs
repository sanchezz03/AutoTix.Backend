using Newtonsoft.Json;

namespace UserService.Application.DTOs.Response.RailwayConnector.Models.AuthResponse;

public class Achievements
{
    [JsonProperty("awards_count")]
    public string AwardsCount { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; }

    [JsonProperty("levels_enabled")]
    public bool LevelsEnabled { get; set; }
}
