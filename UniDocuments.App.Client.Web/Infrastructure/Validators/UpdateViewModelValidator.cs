using FluentValidation;
using UniDocuments.App.Client.Web.Infrastructure.Validators.Common;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account;

namespace UniDocuments.App.Client.Web.Infrastructure.Validators;

public class UpdateViewModelValidator : AbstractValidator<UpdateAccountViewModel>
{
    public UpdateViewModelValidator()
    {
        RuleFor(x => x.FirstName)
            .MinimumLength(2).WithMessage("Имя должно быть не менее 2 симловов")
            .MaximumLength(35).WithMessage("Имя должно быть не более 35 симловов")
            .When(x => string.IsNullOrEmpty(x.FirstName) == false);
        
        RuleFor(x => x.LastName)
            .MinimumLength(2).WithMessage("Фамилия должна быть не менее 2 симловов")
            .MaximumLength(35).WithMessage("Фамилия должна быть не более 35 симловов")
            .When(x => string.IsNullOrEmpty(x.LastName) == false);
        
        RuleFor(x => x.NewPassword)
            .SetValidator(new PasswordValidator()!)
            .When(x => string.IsNullOrEmpty(x.NewPassword) == false);
        
        RuleFor(x => x.NewPasswordConfirm)
            .Equal(x => x.NewPassword)
            .When(x => string.IsNullOrEmpty(x.NewPassword) == false)
            .WithMessage("Пароли не совпадают!");
    }
}