using FluentValidation;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Tasks.Commands.CreateTask;

public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(300).WithMessage("Title must not exceed 300 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters.")
            .When(x => x.Description is not null);

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status must be a valid value (0=ToDo, 1=InProgress, 2=Done).");

        RuleFor(x => x.ProjectId)
            .GreaterThan(0).WithMessage("ProjectId must be a positive integer.");
    }
}
