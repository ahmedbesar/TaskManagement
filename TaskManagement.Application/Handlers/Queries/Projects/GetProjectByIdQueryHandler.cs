using FluentResults;
using MediatR;
using TaskManagement.Application.Queries.Projects;
using TaskManagement.Application.Responses;
using TaskManagement.Application.Mappers;
using TaskManagement.Core.Repositories;

namespace TaskManagement.Application.Handlers.Queries.Projects;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Result<ProjectResponseDto>>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectByIdQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<ProjectResponseDto>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdWithTasksAsync(request.Id);

        if (project == null)
            return Result.Fail($"Project with ID {request.Id} not found.");

        if (project.UserId != request.UserId)
            return Result.Fail("You do not have permission to view this project.");

        var projectDto = ProjectMapper.ToResponseDto(project);
        
        return Result.Ok(projectDto);
    }
}
