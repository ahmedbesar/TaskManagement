using FluentValidation;
using TaskManagement.Application.Commands.Tasks;
using TaskManagement.Core.Consts;

namespace TaskManagement.Application.Validators.Tasks;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Task title is required.")
            .MaximumLength(TaskConstants.TitleMaxLength).WithMessage($"Task title must not exceed {TaskConstants.TitleMaxLength} characters.");

        RuleFor(x => x.Description)
            .MaximumLength(TaskConstants.DescriptionMaxLength).WithMessage($"Task description must not exceed {TaskConstants.DescriptionMaxLength} characters.");

        RuleFor(x => x.ProjectId)
            .GreaterThan(0).WithMessage("Valid Project ID is required.");
            
        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Invalid task priority.");
    }
}
