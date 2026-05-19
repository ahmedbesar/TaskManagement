namespace TaskManagement.Core.Entities;

public class AppUser : EntityBase
{
    public string UserName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;

    public ICollection<Project> Projects { get; private set; } = new List<Project>();

    // EF Core parameterless constructor
    private AppUser() { }

    public static AppUser Create(string userName, string email, string passwordHash)
    {
        return new AppUser
        {
            UserName = userName,
            Email = email,
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow
        };
    }
}
