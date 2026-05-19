using FluentResults;
using MediatR;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Enums;

namespace TaskManagement.Application.Commands.Tasks;

public class UpdateTaskStatusCommand : IRequest<Result>
{
    public int Id { get; set; }
    public TaskItemStatusEnum Status { get; set; }
    public int UserId { get; set; }
}
