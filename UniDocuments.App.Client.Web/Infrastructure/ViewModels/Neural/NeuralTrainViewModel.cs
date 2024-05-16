using System.ComponentModel.DataAnnotations;
using UniDocuments.App.Shared.Documents;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Neural;

public class NeuralTrainViewModel
{
    public DocumentsGlobalData GlobalData { get; set; } = null!;
    
    [Range(4, int.MaxValue)]
    public int EmbeddingSize { get; set; }
    [Range(1, int.MaxValue)]
    public int Epochs { get; set; }
    [Required]
    public string LearningRate { get; set; } = null!;
}