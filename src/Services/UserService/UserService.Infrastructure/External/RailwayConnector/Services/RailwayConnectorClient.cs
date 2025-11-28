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

    public async Task<SendSms> SendSmsAsync(SendSmsRequest request)
    {
        var sendSmsRequest = new GrpcSendSmsRequest { Phone = request.Phone };

        var sendSmsResponse = await _grpcClient.SendSmsAsync(sendSmsRequest);

        return new SendSms
        {
            Success = sendSmsResponse.Success,
            RetryAfter = sendSmsResponse.RetryAfter
        };
    }

    public async Task<Login> LoginAsync(LoginRequest request)
    {
        var grpcRequest = new GrpcLoginRequest
        {
            Phone = request.Phone,
            Code = request.Code,
            Device = new GrpcDeviceInfo
            {
                Name = request.Device?.Name ?? "",
                FcmToken = request.Device?.FcmToken ?? ""
            }
        };

        var grpcResponse = await _grpcClient.LoginAsync(grpcRequest);
        if (grpcResponse == null || grpcResponse.Token == null)
        {
            return new Login();
        }

        var p = grpcResponse.Profile;

        var profile = new Profile
        {
            Id = p.Id,
            Type = p.Type,
            Phone = p.Phone,
            Email = string.IsNullOrEmpty(p.Email) ? null : p.Email,
            ShareText = p.ShareText,

            Passenger = new Passenger
            {
                Id = p.Passenger.Id,
                FirstName = p.Passenger.FirstName,
                LastName = p.Passenger.LastName,
                TicketType = p.Passenger.TicketType,
                PrivilegeId = p.Passenger.PrivilegeId,
                Photo = string.IsNullOrEmpty(p.Passenger.Photo) ? null : p.Passenger.Photo,
                Main = p.Passenger.Main,
                Phone = string.IsNullOrEmpty(p.Passenger.Phone) ? null : p.Passenger.Phone,
                IsShareUser = p.Passenger.IsShareUser,
                Birthday = string.IsNullOrEmpty(p.Passenger.Birthday) ? null : p.Passenger.Birthday,
                Gender = string.IsNullOrEmpty(p.Passenger.Gender) ? null : p.Passenger.Gender,

                PrivilegeData = p.Passenger.PrivilegeData != null
            ? new PrivilegeData
            {
                StudentId = p.Passenger.PrivilegeData.StudentId
            }
            : null,

                Privilege = p.Passenger.Privilege != null
            ? new Privilege
            {
                Id = p.Passenger.Privilege.Id,
                Name = p.Passenger.Privilege.Name,
                Description = p.Passenger.Privilege.Description,
                InputType = p.Passenger.Privilege.InputType,
                Active = p.Passenger.Privilege.Active,
                CompanionId = string.IsNullOrEmpty(p.Passenger.Privilege.CompanionId)
                    ? null
                    : p.Passenger.Privilege.CompanionId,
                Rules = p.Passenger.Privilege.Rules,
                Hint = p.Passenger.Privilege.Hint
            }
            : null
            },

            AdditionalActions = p.AdditionalActions
        ?.Select(a => new AdditionalAction
        {
            Title = a.Title,
            Icon = a.Icon,
            Link = a.Link
        })
        .ToList() ?? new List<AdditionalAction>(),

            Loyalty = p.Loyalty != null
        ? new Loyalty
        {
            Title = p.Loyalty.Title,
            InfoLink = p.Loyalty.InfoLink,
            Icon = p.Loyalty.Icon,
            IconHugs = p.Loyalty.IconHugs,
            PointsCount = p.Loyalty.PointsCount,
            Points = new Points
            {
                Value = p.Loyalty.Points?.Value ?? "",
                Label = p.Loyalty.Points?.Label ?? ""
            },
            DataColumns = p.Loyalty.DataColumns?
                .Select(dc => new DataColumn { Label = dc.Label, Value = dc.Value })
                .ToList() ?? new List<DataColumn>(),
            InfoBlock = p.Loyalty.InfoBlock != null
                ? new InfoBlock
                {
                    Title = p.Loyalty.InfoBlock.Title,
                    Description = p.Loyalty.InfoBlock.Description
                }
                : null
        }
        : null,

            Achievements = p.Achievements != null
        ? new Achievements
        {
            AwardsCount = p.Achievements.AwardsCount,
            Title = p.Achievements.Title,
            Image = p.Achievements.Image,
            LevelsEnabled = p.Achievements.LevelsEnabled
        }
        : null,

            Genders = p.Genders?
        .Select(g => new Gender { Id = g.Id, Title = g.Title })
        .ToList() ?? new List<Gender>(),

            SpecialStatuses = p.SpecialStatuses?.ToList() ?? new List<string>(),

            VerificationType = p.VerificationType,
            AvailableVerificationTypes = p.AvailableVerificationTypes?.ToList() ?? new List<int>()
        };


        return new Login
        {
            Token = new Token
            {
                AccessToken = grpcResponse.Token.AccessToken,
                ExpiresIn = grpcResponse.Token.ExpiresIn
            },
            Profile = profile,
        };
    }

    public async Task LogoutAsync(string accessToken)
    {
        var request = new GrpcLogoutRequest
        {
            AccessToken = accessToken
        };
        await _grpcClient.LogoutAsync(request);
    }
}
