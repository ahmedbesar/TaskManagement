using FluentResults;
using MediatR;

namespace TaskManagement.Application.Commands.Projects;

public class UpdateProjectCommand : IRequest<Result>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int UserId { get; set; }
}
