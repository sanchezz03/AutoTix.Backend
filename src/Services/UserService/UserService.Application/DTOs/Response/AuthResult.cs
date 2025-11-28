using UserService.Application.DTOs.Response.RailwayConnector.Models.AuthResponse;

namespace UserService.Application.DTOs.Response;

public class AuthResult
{
    public string Token { get; set; } = null!;
    public long ExpiresIn { get; set; }
    public Profile Profile { get; set; } = null!;
}
