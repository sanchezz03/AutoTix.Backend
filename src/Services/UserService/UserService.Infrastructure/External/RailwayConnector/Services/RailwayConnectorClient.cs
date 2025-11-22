using UserService.Application.DTOs.Request;
using UserService.Application.DTOs.Response.RailwayConnector.Models.AuthResponse;
using UserService.Application.Interfaces;
using UserService.Infrastructure.Protos;

namespace UserService.Infrastructure.External.RailwayConnector.Services;

public class RailwayConnectorClient : IRailwayConnetorClient
{
    private readonly AuthServiceGrpc.AuthServiceGrpcClient _grpcClient;

    public RailwayConnectorClient(AuthServiceGrpc.AuthServiceGrpcClient grpcClient)
    {
        _grpcClient = grpcClient;
    }

    public async Task<SendSms> SendSmsAsync(Application.DTOs.Request.SendSmsRequest request)
    {
        var sendSmsRequest = new Protos.SendSmsRequest { Phone = request.Phone };

        var sendSmsResponse = await _grpcClient.SendSmsAsync(sendSmsRequest);

        return new SendSms
        {
            Success = sendSmsResponse.Success,
            RetryAfter = sendSmsResponse.RetryAfter
        };
    }
}
