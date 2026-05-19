using FluentResults;
using MediatR;
using TaskManagement.Application.Responses;

namespace TaskManagement.Application.Queries.Projects;

public class GetProjectByIdQuery : IRequest<Result<ProjectResponseDto>>
{
    public int Id { get; set; }
    public int UserId { get; set; }
}
