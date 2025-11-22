using Grpc.Core;
using RailwayConnectorService.API.Protos;
using RailwayConnectorService.Application.Services.Interfaces;
using RailwayConnectorService.Contracts.Models.Uz.Request.Auth;

namespace RailwayConnectorService.API.Grpc;

public class AuthGrpcService : AuthServiceGrpc.AuthServiceGrpcBase
{
    private readonly IAuthService _authService;

    public AuthGrpcService(IAuthService authService)
    {
        _authService = authService;
    }

    public override async Task<GrpcSendSmsResponse> SendSms(GrpcSendSmsRequest request, ServerCallContext context)
    {
        var sendSmsRequest = new SendSmsRequest
        {
            Phone = request.Phone
        };

        var result = await _authService.SendSmsAsync(sendSmsRequest);

        return new GrpcSendSmsResponse
        {
            Success = result.Success,
            RetryAfter = result.RetryAfter
        };
    }

    public override async Task<GrpcLoginResponse> Login(GrpcLoginRequest request, ServerCallContext context)
    {
        var result = await _authService.LoginAsync(new LoginRequest
        {
            Phone = request.Phone,
            Code = request.Code,
            Device = new DeviceInfo
            {
               Name = request.Device.Name,
               FcmToken = request.Device.FcmToken
            }
        });

        if (result == null)
        {
            return new GrpcLoginResponse();
        }

        var profile = result.Profile;

        var protoProfile = new GrpcProfile
        {
            Id = profile.Id,
            Type = profile.Type,
            Phone = profile.Phone,
            Email = profile.Email?.ToString() ?? "",
            ShareText = profile.ShareText,
            Passenger = new GrpcPassenger
            {
                Id = profile.Passenger.Id,
                FirstName = profile.Passenger.FirstName,
                LastName = profile.Passenger.LastName,
                TicketType = profile.Passenger.TicketType,
                PrivilegeId = profile.Passenger.PrivilegeId,
                PrivilegeData = new GrpcPrivilegeData
                {
                    StudentId = profile.Passenger.PrivilegeData?.StudentId ?? ""
                },
                Privilege = new GrpcPrivilege
                {
                    Id = profile.Passenger.Privilege?.Id ?? 0,
                    Name = profile.Passenger.Privilege?.Name ?? "",
                    Description = profile.Passenger.Privilege?.Description ?? "",
                    InputType = profile.Passenger.Privilege?.InputType ?? 0,
                    Active = profile.Passenger.Privilege?.Active ?? false,
                    CompanionId = profile.Passenger.Privilege?.CompanionId?.ToString() ?? "",
                    Rules = profile.Passenger.Privilege?.Rules ?? "",
                    Hint = profile.Passenger.Privilege?.Hint?.ToString() ?? ""
                },
                Photo = profile.Passenger.Photo?.ToString() ?? "",
                Main = profile.Passenger.Main,
                Phone = profile.Passenger.Phone?.ToString() ?? "",
                IsShareUser = profile.Passenger.IsShareUser,
                Birthday = profile.Passenger.Birthday?.ToString() ?? "",
                Gender = profile.Passenger.Gender?.ToString() ?? ""
            },
            AdditionalActions = { profile.AdditionalActions.Select(a => new GrpcAdditionalAction { Title = a.Title, Icon = a.Icon, Link = a.Link }) },
            Loyalty = new GrpcLoyalty
            {
                Title = profile.Loyalty?.Title ?? "",
                InfoLink = profile.Loyalty?.InfoLink ?? "",
                Icon = profile.Loyalty?.Icon ?? "",
                IconHugs = profile.Loyalty?.IconHugs ?? "",
                PointsCount = profile.Loyalty?.PointsCount ?? 0,
                Points = new GrpcLoyaltyPoints { Value = profile.Loyalty?.Points?.Value ?? "", Label = profile.Loyalty?.Points?.Label ?? "" },
                DataColumns = { profile.Loyalty?.DataColumns?.Select(dc => new GrpcDataColumn { Value = dc.Value, Label = dc.Label }) ?? Enumerable.Empty<GrpcDataColumn>() },
                InfoBlock = new GrpcInfoBlock { Title = profile.Loyalty?.InfoBlock?.Title ?? "", Description = profile.Loyalty?.InfoBlock?.Description ?? "" }
            },
            Achievements = new GrpcAchievements
            {
                AwardsCount = profile.Achievements?.AwardsCount ?? "",
                Title = profile.Achievements?.Title ?? "",
                Image = profile.Achievements?.Image ?? "",
                LevelsEnabled = profile.Achievements?.LevelsEnabled ?? false
            },
            Genders = { profile.Genders.Select(g => new GrpcGender { Id = g.Id, Title = g.Title }) },
            SpecialStatuses = { profile.SpecialStatuses?.Select(s => s.ToString()) ?? Enumerable.Empty<string>() },
            VerificationType = profile.VerificationType?.ToString() ?? "",
            AvailableVerificationTypes = { profile.AvailableVerificationTypes ?? Enumerable.Empty<int>() }
        };

        return new GrpcLoginResponse
        {
            Token = new GrpcToken
            {
                AccessToken = result.Token.AccessToken,
                ExpiresIn = result.Token.ExpiresIn
            },
            Profile = protoProfile
        };
    }

    public override async Task<Empty> Logout(Empty request, ServerCallContext context)
    {
        await _authService.LogoutAsync();
        return new Empty();
    }
}
