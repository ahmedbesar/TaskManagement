namespace TaskManagement.Core.Entities;

public class Project : EntityBase
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public int UserId { get; private set; }
    public AppUser User { get; private set; } = null!;

    public ICollection<TaskItem> Tasks { get; private set; } = new List<TaskItem>();

    // EF Core requires a parameterless constructor
    private Project() { }

    public static Project Create(string name, string description, int userId)
    {
        return new Project
        {
            Name = name,
            Description = description,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
