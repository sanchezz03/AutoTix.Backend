namespace UserService.Infrastructure.Configuration;

public class JwtOptions
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpiresSeconds { get; set; }
}
