using TaskManagement.Application.Commands.Tasks;
using TaskManagement.Application.Responses;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Enums;

namespace TaskManagement.Application.Mappers;

public static class TaskMapper
{
    public static TaskResponseDto ToResponseDto(TaskItem task) => new TaskResponseDto(
        Id: task.Id,
        Title: task.Title,
        Description: task.Description,
        Status: task.Status.ToString(),
        DueDate: task.DueDate,
        Priority: task.Priority.ToString(),
        ProjectId: task.ProjectId,
        CreatedAt: task.CreatedAt
    );

    public static List<TaskResponseDto> ToResponseDtoList(IEnumerable<TaskItem> tasks) =>
        tasks.Select(ToResponseDto).ToList();

    public static TaskItem ToEntity(CreateTaskCommand cmd) =>
        TaskItem.Create(cmd.Title, cmd.Description, cmd.DueDate, (TaskItemPriorityEnum)cmd.Priority, cmd.ProjectId);
}
