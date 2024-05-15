using UniDocuments.App.Shared.Documents;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Neural;

public class NeuralTrainViewModel
{
    public DocumentsGlobalData GlobalData { get; set; } = null!;
    public int EmbeddingSize { get; set; }
    public int Epochs { get; set; }
    public string LearningRate { get; set; } = null!;
}