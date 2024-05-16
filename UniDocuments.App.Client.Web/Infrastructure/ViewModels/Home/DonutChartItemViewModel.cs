namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Home;

public class DonutChartItemViewModel
{
    public DonutChartItemViewModel(string label, double value, string color)
    {
        Label = label;
        Value = value;
        Color = color;
    }

    public string Label { get; init; }
    public double Value { get; init; }
    public string Color { get; init; }
}