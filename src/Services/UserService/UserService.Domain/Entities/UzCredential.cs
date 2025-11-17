namespace UserService.Domain.Entities;

public class UzCredential : Base<long>
{
    public string EncryptedAccessToken { get; set; } = null!;
    public DateTimeOffset IssuedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public string? RawProfileJson { get; set; }
    public string DeviceName { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public DateTimeOffset CreatedAt { get; set; }

    #region Navigation Properties

    public long UserId { get; set; }
    public User User { get; set; } = null!;

    #endregion
}
