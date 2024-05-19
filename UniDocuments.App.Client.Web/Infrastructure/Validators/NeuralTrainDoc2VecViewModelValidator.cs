using System.Globalization;
using FluentValidation;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Neural;

namespace UniDocuments.App.Client.Web.Infrastructure.Validators;

public class NeuralTrainDoc2VecViewModelValidator : AbstractValidator<NeuralTrainDoc2VecViewModel>
{
    public NeuralTrainDoc2VecViewModelValidator()
    {
        RuleFor(x => x.LearningRate)
            .Must(x => float.TryParse(x, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
            .When(x => x is not null)
            .WithMessage("Alpha должна быть числом");
        
        RuleFor(x => x.MinAlpha)
            .Must(x => float.TryParse(x, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
            .When(x => x is not null)
            .WithMessage("MinAlpha должна быть числом");

        RuleFor(x => float.Parse(x.MinAlpha, NumberStyles.Float, CultureInfo.InvariantCulture))
            .LessThan(x => float.Parse(x.LearningRate, NumberStyles.Float, CultureInfo.InvariantCulture))
            .When(x => x is not null)
            .WithMessage("MinAlpha должна быть меньше Alpha");
    }
}