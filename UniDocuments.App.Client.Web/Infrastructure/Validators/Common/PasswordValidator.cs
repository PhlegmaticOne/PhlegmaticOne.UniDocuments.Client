using FluentValidation;

namespace UniDocuments.App.Client.Web.Infrastructure.Validators.Common;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(p => p)
            .MinimumLength(8).WithMessage("Пароль должен быть минимум из 8 символов")
            .MaximumLength(32).WithMessage("Пароль должен быть максимум из 32 символов")
            .Matches("[A-Z]+").WithMessage("Пароль должен содержать минимум 1 символ в верхнем регистре")
            .Matches("[a-z]+").WithMessage("Пароль должен содержать минимум 1 символ в нижнем регистре")
            .Matches("[0-9]+").WithMessage("Пароль должен содержать минимум 1 число");
    }
}