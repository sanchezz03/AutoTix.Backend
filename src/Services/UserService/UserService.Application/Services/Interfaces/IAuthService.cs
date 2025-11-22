namespace UserService.Application.Services.Interfaces;

public interface IAuthService
{
    Task<UzResponse<SendSms>> SendSmsAsync(SendSmsRequest request);
    Task<UzResponse<Login>> LoginAsync(LoginRequest request);
    Task LogoutAsync();
}
