using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Commands.Projects;
using TaskManagement.Application.Queries.Projects;
using TaskManagement.Application.Responses;
using TaskManagement.Application.Wrappers;

namespace TaskManagement.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllProjectsQuery { UserId = GetUserId() };
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(ApiResponse<List<ProjectResponseDto>>.Success(result.Value));
        }

        return BadRequest(ApiResponse<List<ProjectResponseDto>>.Failure(result.Errors.Select(e => e.Message)));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetProjectByIdQuery { Id = id, UserId = GetUserId() };
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(ApiResponse<ProjectResponseDto>.Success(result.Value));
        }

        return NotFound(ApiResponse<ProjectResponseDto>.Failure(result.Errors.Select(e => e.Message)));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectCommand command)
    {
        command.UserId = GetUserId();
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, ApiResponse<int>.Success(result.Value, "Project created successfully."));
        }

        return BadRequest(ApiResponse<int>.Failure(result.Errors.Select(e => e.Message)));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateProjectCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(ApiResponse<object>.Failure("ID mismatch."));
        }

        command.UserId = GetUserId();
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok(ApiResponse<object>.Success(new object(), "Project updated successfully."));
        }

        return BadRequest(ApiResponse<object>.Failure(result.Errors.Select(e => e.Message)));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteProjectCommand { Id = id, UserId = GetUserId() };
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok(ApiResponse<object>.Success(new object(), "Project deleted successfully."));
        }

        return BadRequest(ApiResponse<object>.Failure(result.Errors.Select(e => e.Message)));
    }

    private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}
