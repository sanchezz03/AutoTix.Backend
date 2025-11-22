using Newtonsoft.Json;

namespace UserService.Application.DTOs.Response.RailwayConnector.Models.AuthResponse;

public class Login
{
    [JsonProperty("token")]
    public Token Token { get; set; }

    [JsonProperty("profile")]
    public Profile Profile { get; set; }
}
