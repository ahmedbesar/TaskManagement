using FluentResults;
using MediatR;
using TaskManagement.Application.Commands.Tasks;
using TaskManagement.Application.Mappers;
using TaskManagement.Core.Repositories;

namespace TaskManagement.Application.Handlers.Commands.Tasks;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result<int>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IProjectRepository _projectRepository;

    public CreateTaskCommandHandler(ITaskRepository taskRepository, IProjectRepository projectRepository)
    {
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
    }

    public async Task<Result<int>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId);

        if (project == null)
            return Result.Fail($"Project with ID {request.ProjectId} not found.");

        if (project.UserId != request.UserId)
            return Result.Fail("You do not have permission to add tasks to this project.");

        var task = TaskMapper.ToEntity(request);
        var createdTask = await _taskRepository.AddAsync(task);
        
        return Result.Ok(createdTask.Id);
    }
}
