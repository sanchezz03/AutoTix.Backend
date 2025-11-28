namespace UserService.Application.Interfaces;

public interface IEncryptionService
{
    string Protect(string plaintext);
    string Unprotect(string protectedText);
}
