using FluentValidation;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Activities;

namespace UniDocuments.App.Client.Web.Infrastructure.Validators;

public class ActivityCreateValidator : AbstractValidator<ActivityCreateViewModel>
{
    public ActivityCreateValidator()
    {
        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate)
            .WithMessage("Начало активности должно быть раньше ее окончания");

        RuleFor(x => x.EndDate)
            .GreaterThan(DateTime.Now)
            .WithMessage("Активность не может закончиться раньше текущего дня");

        RuleFor(x => x.Students.Count)
            .GreaterThan(0)
            .WithMessage("В активности должен быть хотя бы один студент");
    }
}