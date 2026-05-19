using FluentResults;
using MediatR;

namespace TaskManagement.Application.Commands.Auth;

public class LoginCommand : IRequest<Result<string>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
