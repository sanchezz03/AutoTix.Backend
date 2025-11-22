namespace UserService.Domain.Entities;

public class User : Base<long>
{
    public string Phone { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }

    #region Navigation Properties   

    public ICollection<UzCredential> UzCredentials { get; set; } = new List<UzCredential>();

    #endregion
}
