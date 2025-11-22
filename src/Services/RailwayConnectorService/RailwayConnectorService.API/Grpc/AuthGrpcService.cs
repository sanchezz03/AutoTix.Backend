using RailwayConnectorService.API.Protos;
using RailwayConnectorService.Application.Services.Interfaces;

namespace RailwayConnectorService.API.Grpc;

public class AuthGrpcService : AuthServiceGrpc.AuthServiceGrpcBase
{
    private readonly IAuthService _authService;

    public AuthGrpcService(IAuthService authService)
    {
        _authService = authService;
    }


}
