using FluentValidation;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Document;

namespace UniDocuments.App.Client.Web.Infrastructure.Validators;

public class DocumentSearchViewModelValidator : AbstractValidator<DocumentsSearchViewModel>
{
    public DocumentSearchViewModelValidator()
    {
        RuleFor(x => x.Count).GreaterThan(0).LessThan(15);
        RuleFor(x => x.Phrase).NotNull().NotEmpty();
        RuleFor(x => x.ModelName).NotNull().NotEmpty();
    }
}