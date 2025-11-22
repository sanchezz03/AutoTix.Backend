using UserService.Application.DTOs.Request;
using UserService.Application.DTOs.Response.RailwayConnector.Models.AuthResponse;

namespace UserService.Application.Interfaces;

public interface IRailwayConnetorClient
{
    Task<SendSms> SendSmsAsync(SendSmsRequest request);
}
