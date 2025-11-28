using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Application.DTOs.Request;
using UserService.Application.Services.Interfaces;

namespace UserService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthController(IAuthService service, IHttpContextAccessor httpContextAccessor)
    {
        _service = service;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost("send-message")]
    public async Task<IActionResult> SendMessage([FromBody] SendSmsRequest request)
    {
        var result = await _service.SendSmsAsync(request);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _service.LoginAsync(request);
        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        //var currentUserId = _httpContextAccessor.HttpContext?.User
        //    .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //if (string.IsNullOrEmpty(currentUserId))
        //    return Unauthorized("User ID missing in token");

        //long userId = long.Parse(currentUserId);
        await _service.LogoutAsync(1);
        return NoContent();
    }
}
