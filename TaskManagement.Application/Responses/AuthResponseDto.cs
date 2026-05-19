namespace TaskManagement.Application.Responses;

public readonly record struct AuthResponseDto(string Token, string UserName, string Email);
