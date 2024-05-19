using System.Globalization;
using FluentValidation;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Neural;

namespace UniDocuments.App.Client.Web.Infrastructure.Validators;

public class NeuralTrainKerasViewModelValidator : AbstractValidator<NeuralTrainKerasViewModel>
{
    public NeuralTrainKerasViewModelValidator()
    {
        RuleFor(x => x.LearningRate)
            .Must(x => float.TryParse(x, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
            .WithMessage("Скорость обучения должна быть числом");

        RuleFor(x => x.WindowSize)
            .Must(x => x % 2 == 0)
            .WithMessage("Размер окна должен быть нечетным числом");
    }
}