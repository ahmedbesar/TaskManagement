using FluentResults;
using MediatR;
using TaskManagement.Application.Commands.Projects;
using TaskManagement.Application.Mappers;
using TaskManagement.Core.Repositories;

namespace TaskManagement.Application.Handlers.Commands.Projects;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<int>>
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<int>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = ProjectMapper.ToEntity(request);
        var createdProject = await _projectRepository.AddAsync(project);
        
        return Result.Ok(createdProject.Id);
    }
}
