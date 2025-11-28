using Microsoft.AspNetCore.DataProtection;
using UserService.Application.Interfaces;

namespace UserService.Infrastructure.Security;

public class DataProtectionEncryptionService : IEncryptionService
{
    private readonly IDataProtector _protector;
  
    public DataProtectionEncryptionService(IDataProtectionProvider provider)
    {
        _protector = provider.CreateProtector("UzAccessTokenProtection");
    }

    public string Protect(string plaintext) => _protector.Protect(plaintext);

    public string Unprotect(string protectedText) => _protector.Unprotect(protectedText);
}
