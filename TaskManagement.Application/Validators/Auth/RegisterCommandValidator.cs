using FluentValidation;
using TaskManagement.Application.Commands.Auth;
using TaskManagement.Core.Consts;

namespace TaskManagement.Application.Validators.Auth;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(UserConstants.UserNameMaxLength).WithMessage($"Username must not exceed {UserConstants.UserNameMaxLength} characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.")
            .MaximumLength(UserConstants.EmailMaxLength).WithMessage($"Email must not exceed {UserConstants.EmailMaxLength} characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(UserConstants.PasswordMinLength).WithMessage($"Password must be at least {UserConstants.PasswordMinLength} characters long.");
    }
}
