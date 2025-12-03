namespace UserService.Application.DTOs.Response;

public class UzAccessTokenResult
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTimeOffset ExpiresAt { get; set; }
}
