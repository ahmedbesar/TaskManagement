namespace TaskManagement.Core.Entities;

public class ProjectEntity
{
    private ProjectEntity()
    {
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = default!;

    public string Description { get; private set; } = default!;

    public DateTime CreatedAt { get; private set; }

    public ICollection<TaskEntity> Tasks { get; private set; }
        = new List<TaskEntity>();


    public static ProjectEntity Create(
        string name,
        string description)
    {
        return new ProjectEntity
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            CreatedAt = DateTime.UtcNow
        };
    }


    public void Update(
        string name,
        string description)
    {
        Name = name;
        Description = description;
    }
}