namespace UserService.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(long userId, string phone, out long expiresInSeconds);
}
