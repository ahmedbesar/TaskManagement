using FluentResults;
using MediatR;
using TaskManagement.Application.Queries.Tasks;
using TaskManagement.Application.Responses;
using TaskManagement.Application.Mappers;
using TaskManagement.Core.Repositories;

namespace TaskManagement.Application.Handlers.Queries.Tasks;

public class GetTasksByProjectQueryHandler : IRequestHandler<GetTasksByProjectQuery, Result<List<TaskResponseDto>>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IProjectRepository _projectRepository;

    public GetTasksByProjectQueryHandler(ITaskRepository taskRepository, IProjectRepository projectRepository)
    {
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
    }

    public async Task<Result<List<TaskResponseDto>>> Handle(GetTasksByProjectQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId);

        if (project == null)
            return Result.Fail($"Project with ID {request.ProjectId} not found.");

        if (project.UserId != request.UserId)
            return Result.Fail("You do not have permission to view tasks for this project.");

        var tasks = await _taskRepository.GetByProjectIdAsync(request.ProjectId);
        var taskDtos = TaskMapper.ToResponseDtoList(tasks);
        
        return Result.Ok(taskDtos);
    }
}
