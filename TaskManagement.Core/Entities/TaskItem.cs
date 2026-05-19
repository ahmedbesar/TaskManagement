using TaskManagement.Core.Enums;

namespace TaskManagement.Core.Entities;

public class TaskItem : EntityBase
{
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public TaskItemStatusEnum Status { get; private set; } = TaskItemStatusEnum.Todo;
    public DateTime? DueDate { get; private set; }
    public TaskItemPriorityEnum Priority { get; private set; } = TaskItemPriorityEnum.Medium;

    public int ProjectId { get; private set; }
    public Project Project { get; private set; } = null!;

    // EF Core requires a parameterless constructor
    private TaskItem() { }

    public static TaskItem Create(string title, string description, DateTime? dueDate, TaskItemPriorityEnum priority, int projectId)
    {
        return new TaskItem
        {
            Title = title,
            Description = description,
            DueDate = dueDate,
            Priority = priority,
            ProjectId = projectId,
            Status = TaskItemStatusEnum.Todo,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateStatus(TaskItemStatusEnum status)
    {
        Status = status;
    }
}
