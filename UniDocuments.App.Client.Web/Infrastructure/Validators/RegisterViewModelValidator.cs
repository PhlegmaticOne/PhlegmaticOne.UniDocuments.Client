using FluentValidation;
using UniDocuments.App.Client.Web.Infrastructure.Validators.Common;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account;

namespace UniDocuments.App.Client.Web.Infrastructure.Validators;

public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
{
    public RegisterViewModelValidator()
    {
        RuleFor(x => x.UserName).EmailAddress().WithMessage("Невалидная электронная почта!");
        
        RuleFor(x => x.Password).SetValidator(new PasswordValidator());
        
        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage("Пароли не совпадают!");
    }
}