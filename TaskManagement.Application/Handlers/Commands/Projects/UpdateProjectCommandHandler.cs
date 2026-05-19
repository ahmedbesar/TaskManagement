using FluentResults;
using MediatR;
using TaskManagement.Application.Commands.Projects;
using TaskManagement.Application.Mappers;
using TaskManagement.Core.Repositories;

namespace TaskManagement.Application.Handlers.Commands.Projects;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Result>
{
    private readonly IProjectRepository _projectRepository;

    public UpdateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);

        if (project == null)
            return Result.Fail($"Project with ID {request.Id} not found.");

        if (project.UserId != request.UserId)
            return Result.Fail("You do not have permission to update this project.");

        project.Update(request.Name, request.Description);
        await _projectRepository.UpdateAsync(project);

        return Result.Ok();
    }
}
