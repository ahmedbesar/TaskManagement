using FluentResults;
using MediatR;
using TaskManagement.Application.Queries.Projects;
using TaskManagement.Application.Responses;
using TaskManagement.Application.Mappers;
using TaskManagement.Core.Repositories;

namespace TaskManagement.Application.Handlers.Queries.Projects;

public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, Result<List<ProjectResponseDto>>>
{
    private readonly IProjectRepository _projectRepository;

    public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<List<ProjectResponseDto>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetByUserIdAsync(request.UserId);
        var projectDtos = ProjectMapper.ToResponseDtoList(projects);
        
        return Result.Ok(projectDtos);
    }
}
