using FluentResults;
using MediatR;

namespace TaskManagement.Application.Commands.Auth;

public class RegisterCommand : IRequest<Result<string>>
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
