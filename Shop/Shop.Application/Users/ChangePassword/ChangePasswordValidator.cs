using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Users.ChangePassword;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordValidator()
    {
        RuleFor(r => r.Pass)
            .NotNull().NotEmpty().MinimumLength(6).WithMessage(ValidationMessages.required("کلمه عبور نامعتبر"));
    }
}