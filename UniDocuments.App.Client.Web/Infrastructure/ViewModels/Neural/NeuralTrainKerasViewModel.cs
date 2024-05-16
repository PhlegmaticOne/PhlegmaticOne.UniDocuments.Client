using System.ComponentModel.DataAnnotations;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Neural;

public class NeuralTrainKerasViewModel : NeuralTrainViewModel
{
    [Range(3, int.MaxValue)]
    public int WindowSize { get; set; }
    [Range(3, int.MaxValue)]
    public int BatchSize { get; set; }
}