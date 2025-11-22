using Newtonsoft.Json;

namespace UserService.Infrastructure.External.RailwayConnector.Models.AuthResponse;

public class Token
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
}
