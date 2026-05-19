namespace TaskManagement.Core.Entities;

public class TaskEntity
{
    private TaskEntity()
    {
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; } = default!;

    public string Description { get; private set; } = default!;

    public string Status { get; private set; } = default!;

    public DateTime DueDate { get; private set; }

    public string Priority { get; private set; } = default!;

    public Guid ProjectId { get; private set; }

    public ProjectEntity Project { get; private set; } = default!;


    public static TaskEntity Create(
        string title,
        string description,
        string status,
        DateTime dueDate,
        string priority,
        Guid projectId)
    {
        return new TaskEntity
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Status = status,
            DueDate = dueDate,
            Priority = priority,
            ProjectId = projectId
        };
    }


    public void UpdateStatus(string status)
    {
        Status = status;
    }
}