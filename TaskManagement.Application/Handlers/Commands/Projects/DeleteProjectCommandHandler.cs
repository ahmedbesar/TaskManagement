using FluentResults;
using MediatR;
using TaskManagement.Application.Commands.Projects;
using TaskManagement.Core.Repositories;

namespace TaskManagement.Application.Handlers.Commands.Projects;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Result>
{
    private readonly IProjectRepository _projectRepository;

    public DeleteProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);

        if (project == null)
            return Result.Fail($"Project with ID {request.Id} not found.");

        if (project.UserId != request.UserId)
            return Result.Fail("You do not have permission to delete this project.");

        await _projectRepository.DeleteAsync(project);

        return Result.Ok();
    }
}
