using Newtonsoft.Json;

namespace UserService.Application.DTOs.Response.RailwayConnector.Models.AuthResponse;

public class SendSms
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("retry_after")]
    public int RetryAfter { get; set; }
}
