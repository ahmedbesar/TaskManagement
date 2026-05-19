using FluentResults;
using MediatR;
using TaskManagement.Application.Commands.Auth;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.Repositories;

namespace TaskManagement.Application.Handlers.Commands.Auth;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginCommandHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return Result.Fail("Invalid email or password.");
        }

        var token = _jwtTokenService.GenerateToken(user);
        return Result.Ok(token);
    }
}
