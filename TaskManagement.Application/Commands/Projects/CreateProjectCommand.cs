using FluentResults;
using MediatR;

namespace TaskManagement.Application.Commands.Projects;

public class CreateProjectCommand : IRequest<Result<int>>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int UserId { get; set; }
}
