using FluentResults;
using MediatR;
using TaskManagement.Application.Commands.Tasks;
using TaskManagement.Core.Repositories;

namespace TaskManagement.Application.Handlers.Commands.Tasks;

public class UpdateTaskStatusCommandHandler : IRequestHandler<UpdateTaskStatusCommand, Result>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IProjectRepository _projectRepository;

    public UpdateTaskStatusCommandHandler(ITaskRepository taskRepository, IProjectRepository projectRepository)
    {
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
    }

    public async Task<Result> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);

        if (task == null)
            return Result.Fail($"Task with ID {request.Id} not found.");

        var project = await _projectRepository.GetByIdAsync(task.ProjectId);

        if (project == null || project.UserId != request.UserId)
            return Result.Fail("You do not have permission to update this task.");

        task.UpdateStatus(request.Status);
        await _taskRepository.UpdateAsync(task);

        return Result.Ok();
    }
}
