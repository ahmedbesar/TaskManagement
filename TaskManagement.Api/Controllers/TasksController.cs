using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Commands.Tasks;
using TaskManagement.Application.Queries.Tasks;
using TaskManagement.Application.Responses;
using TaskManagement.Application.Wrappers;

namespace TaskManagement.Api.Controllers;

[Authorize]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("api/projects/{projectId}/tasks")]
    public async Task<IActionResult> GetByProject(int projectId)
    {
        var query = new GetTasksByProjectQuery { ProjectId = projectId, UserId = GetUserId() };
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(ApiResponse<List<TaskResponseDto>>.Success(result.Value));
        }

        return BadRequest(ApiResponse<List<TaskResponseDto>>.Failure(result.Errors.Select(e => e.Message)));
    }

    [HttpPost("api/projects/{projectId}/tasks")]
    public async Task<IActionResult> Create(int projectId, CreateTaskCommand command)
    {
        if (projectId != command.ProjectId)
        {
            return BadRequest(ApiResponse<object>.Failure("Project ID mismatch."));
        }

        command.UserId = GetUserId();
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Created(string.Empty, ApiResponse<int>.Success(result.Value, "Task created successfully."));
        }

        return BadRequest(ApiResponse<int>.Failure(result.Errors.Select(e => e.Message)));
    }

    [HttpPut("api/tasks/{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, UpdateTaskStatusCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(ApiResponse<object>.Failure("Task ID mismatch."));
        }

        command.UserId = GetUserId();
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok(ApiResponse<object>.Success(new object(), "Task status updated successfully."));
        }

        return BadRequest(ApiResponse<object>.Failure(result.Errors.Select(e => e.Message)));
    }

    [HttpDelete("api/tasks/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteTaskCommand { Id = id, UserId = GetUserId() };
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok(ApiResponse<object>.Success(new object(), "Task deleted successfully."));
        }

        return BadRequest(ApiResponse<object>.Failure(result.Errors.Select(e => e.Message)));
    }

    private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}
