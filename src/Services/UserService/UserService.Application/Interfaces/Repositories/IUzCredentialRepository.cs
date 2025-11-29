using UserService.Domain.Entities;

namespace UserService.Application.Interfaces.Repositories;

public interface IUzCredentialRepository
{
    Task AddAsync(UzCredential credential);
    Task<UzCredential?> GetActiveCredentialAsync(long userId);
    Task DeactivateUserCredentialsAsync(long userId);
}