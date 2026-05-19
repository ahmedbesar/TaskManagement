using FluentResults;
using MediatR;

namespace TaskManagement.Application.Commands.Projects;

public class DeleteProjectCommand : IRequest<Result>
{
    public int Id { get; set; }
    public int UserId { get; set; }
}
