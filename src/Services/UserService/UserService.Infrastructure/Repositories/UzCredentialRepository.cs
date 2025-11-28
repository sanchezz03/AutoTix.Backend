using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces.Repositories;
using UserService.Domain.Entities;
using UserService.Infrastructure.Persistence;

namespace UserService.Infrastructure.Repositories;

public class UzCredentialRepository : IUzCredentialRepository
{
    private readonly AppDbContext _db;

    public UzCredentialRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(UzCredential credential)
    {
        _db.UzCredentials.Add(credential);
        await SaveChangesAsync();
    }

    public async Task<UzCredential?> GetActiveCredentialAsync(long userId)
    {
        var query = _db.UzCredentials
            .Where(c => c.UserId == userId && c.IsActive);

        return await query.OrderByDescending(c => c.CreatedAt)
                          .FirstOrDefaultAsync();
    }

    public async Task DeactivateUserCredentialsAsync(long userId)
    {
        var creds = await _db.UzCredentials
            .Where(x => x.UserId == userId && x.IsActive)
            .ToListAsync();

        foreach (var c in creds)
            c.IsActive = false;

        await SaveChangesAsync();
    }

    #region Private methods

    private Task SaveChangesAsync() => _db.SaveChangesAsync();

    #endregion
}