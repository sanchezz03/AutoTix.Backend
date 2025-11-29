using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Application.Interfaces;
using UserService.Infrastructure.Configuration;

namespace UserService.Infrastructure.Security;

public class JwtService : IJwtService
{
    private readonly JwtOptions _opt;
    private readonly byte[] _key;

    public JwtService(IOptions<JwtOptions> options)
    {
        _opt = options.Value;
        _key = Encoding.UTF8.GetBytes(_opt.Secret);
    }

    public string GenerateToken(long userId, string phone, out long expiresInSeconds)
    {
        expiresInSeconds = _opt.ExpiresSeconds;

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim("phone", phone),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var creds = new SigningCredentials(
            new SymmetricSecurityKey(_key),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: _opt.Issuer,
            audience: _opt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddSeconds(_opt.ExpiresSeconds),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
