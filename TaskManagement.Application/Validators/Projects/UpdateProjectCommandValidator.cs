using FluentValidation;
using TaskManagement.Application.Commands.Projects;
using TaskManagement.Core.Consts;

namespace TaskManagement.Application.Validators.Projects;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Valid Project ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Project name is required.")
            .MaximumLength(ProjectConstants.NameMaxLength).WithMessage($"Project name must not exceed {ProjectConstants.NameMaxLength} characters.");

        RuleFor(x => x.Description)
            .MaximumLength(ProjectConstants.DescriptionMaxLength).WithMessage($"Project description must not exceed {ProjectConstants.DescriptionMaxLength} characters.");
    }
}
