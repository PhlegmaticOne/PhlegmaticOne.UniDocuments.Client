using UniDocuments.App.Shared.Neural;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Neural;

public class NeuralTrainKerasViewModel : NeuralTrainViewModel
{
    public NeuralTrainResultKeras? TrainResult { get; set; }
    public int WindowSize { get; set; }
    public int BatchSize { get; set; }
}