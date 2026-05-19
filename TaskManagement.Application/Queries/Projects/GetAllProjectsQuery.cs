using FluentResults;
using MediatR;
using TaskManagement.Application.Responses;

namespace TaskManagement.Application.Queries.Projects;

public class GetAllProjectsQuery : IRequest<Result<List<ProjectResponseDto>>>
{
    public int UserId { get; set; }
}
