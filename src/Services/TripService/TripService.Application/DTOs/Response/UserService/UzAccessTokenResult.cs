namespace TripService.Application.DTOs.Response.UserService;

public class UzAccessTokenResult
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTimeOffset ExpiresAt { get; set; }
}
