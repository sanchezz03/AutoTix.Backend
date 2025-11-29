using UserService.Domain.Entities;

namespace UserService.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByPhoneAsync(string phone);
    Task<User> AddAsync(User user);
    Task SaveChangesAsync();
}