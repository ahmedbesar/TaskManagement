using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Commands.Auth;
using TaskManagement.Application.Responses;
using TaskManagement.Application.Wrappers;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            var responseDto = new AuthResponseDto
            {
                Token = result.Value,
                UserName = command.UserName,
                Email = command.Email
            };
            return Created(string.Empty, ApiResponse<AuthResponseDto>.Success(responseDto, "Registration successful."));
        }

        return BadRequest(ApiResponse<AuthResponseDto>.Failure(result.Errors.Select(e => e.Message)));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            var responseDto = new AuthResponseDto
            {
                Token = result.Value,
                Email = command.Email
            };
            return Ok(ApiResponse<AuthResponseDto>.Success(responseDto, "Login successful."));
        }

        return Unauthorized(ApiResponse<AuthResponseDto>.Failure(result.Errors.Select(e => e.Message)));
    }
}
