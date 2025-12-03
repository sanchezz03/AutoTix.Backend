using Microsoft.Win32.SafeHandles;
using UserService.Application.DTOs.Request;
using UserService.Application.DTOs.Response;
using UserService.Application.DTOs.Response.RailwayConnector.Models.AuthResponse;
using UserService.Application.Interfaces;
using UserService.Application.Interfaces.Repositories;
using UserService.Application.Services.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.Services;

public class AuthService : IAuthService
{
    private readonly IRailwayConnetorClient _railwayConnetorClient;
    private readonly IUserRepository _users;
    private readonly IUzCredentialRepository _credentials;
    private readonly IEncryptionService _encryption;
    private readonly IJwtService _jwt;

    public AuthService(
        IRailwayConnetorClient railwayConnectorClient,
        IUserRepository users,
        IUzCredentialRepository credentials,
        IEncryptionService encryption,
        IJwtService jwt)
    {
        _railwayConnetorClient = railwayConnectorClient;
        _users = users;
        _credentials = credentials;
        _encryption = encryption;
        _jwt = jwt;
    }

    public async Task<SendSms> SendSmsAsync(SendSmsRequest request)
    {
        return await _railwayConnetorClient.SendSmsAsync(request);
    }
    public async Task<AuthResult> LoginAsync(LoginRequest request)
    {
        var uzLogin = await _railwayConnetorClient.LoginAsync(request);
        if (uzLogin == null || uzLogin.Token == null)
        {
            throw new InvalidOperationException("Login failed");
        }

        var phone = uzLogin.Profile.Phone ?? request.Phone;

        var user = await _users.GetByPhoneAsync(phone);
        if (user == null)
        {
            user = await _users.AddAsync(new User { Phone = phone, CreatedAt = DateTimeOffset.UtcNow });
        }

        await _credentials.DeactivateUserCredentialsAsync(user.Id);

        var token = uzLogin.Token.AccessToken;
        var encryptedToken = _encryption.Protect(token);

        var expires = DateTimeOffset.UtcNow.AddSeconds(uzLogin.Token.ExpiresIn);

        await _credentials.AddAsync(new UzCredential
        {
            UserId = user.Id,
            EncryptedAccessToken = encryptedToken,
            ExpiresAt = expires,
            CreatedAt = DateTimeOffset.UtcNow,
            IsActive = true,
            RawProfileJson = uzLogin.Profile != null
              ? Newtonsoft.Json.JsonConvert.SerializeObject(uzLogin.Profile)
              : null,
            DeviceName = request.Device?.Name ?? "unknown"
        });

        var jwt = _jwt.GenerateToken(user.Id, phone, out var jwtExpiresIn);

        return new AuthResult
        {
            Token = jwt,
            ExpiresIn = jwtExpiresIn,
            Profile = uzLogin.Profile
        };
    }

    public async Task<UzAccessTokenResult> GetUzAccessTokenAsync(long userId)
    {
        var credential = await _credentials.GetActiveCredentialAsync(userId);
        if (credential == null)
        {
            throw new InvalidOperationException($"No active Railway token for user {userId}");
        }

        var token = _encryption.Unprotect(credential.EncryptedAccessToken);

        return new UzAccessTokenResult
        {
            AccessToken = token,
            ExpiresAt = credential.ExpiresAt
        };
    }

    public async Task LogoutAsync(long userId)
    {
        var credential = await _credentials.GetActiveCredentialAsync(userId);
        if (credential == null)
        {
            return;
        }

        var accessToken = _encryption.Unprotect(credential.EncryptedAccessToken);
        await _railwayConnetorClient.LogoutAsync(accessToken);
        await _credentials.DeactivateUserCredentialsAsync(userId);
    }
}
