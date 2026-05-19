using FluentValidation;
using TaskManagement.Application.Commands.Tasks;

namespace TaskManagement.Application.Validators.Tasks;

public class UpdateTaskStatusCommandValidator : AbstractValidator<UpdateTaskStatusCommand>
{
    public UpdateTaskStatusCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Valid Task ID is required.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid task status.");
    }
}
