using FluentResults;
using MediatR;
using TaskManagement.Application.Commands.Auth;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.Repositories;

namespace TaskManagement.Application.Handlers.Commands.Auth;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var emailExists = await _userRepository.EmailExistsAsync(request.Email);
        if (emailExists)
            return Result.Fail("Email is already registered.");

        var user = AppUser.Create(
            request.UserName,
            request.Email,
            BCrypt.Net.BCrypt.HashPassword(request.Password)
        );

        await _userRepository.AddAsync(user);

        var token = _jwtTokenService.GenerateToken(user);
        return Result.Ok(token);
    }
}
