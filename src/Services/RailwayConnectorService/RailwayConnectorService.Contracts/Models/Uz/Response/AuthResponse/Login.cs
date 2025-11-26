using Newtonsoft.Json;

namespace RailwayConnectorService.Contracts.Models.Uz.Response.AuthResponse;

public class Login
{
    [JsonProperty("token")]
    public Token Token { get; set; }

    [JsonProperty("profile")]
    public Profile Profile { get; set; }
}
