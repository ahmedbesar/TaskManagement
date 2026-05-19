using FluentResults;
using MediatR;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Enums;

namespace TaskManagement.Application.Commands.Tasks;

public class CreateTaskCommand : IRequest<Result<int>>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public TaskItemPriorityEnum Priority { get; set; } = TaskItemPriorityEnum.Medium;
    public int ProjectId { get; set; }
    public int UserId { get; set; }
}
