using FluentResults;
using MediatR;
using TaskManagement.Application.Responses;

namespace TaskManagement.Application.Queries.Tasks;

public class GetTasksByProjectQuery : IRequest<Result<List<TaskResponseDto>>>
{
    public int ProjectId { get; set; }
    public int UserId { get; set; }
}
