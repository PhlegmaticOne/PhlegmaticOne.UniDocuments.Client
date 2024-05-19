using System.ComponentModel.DataAnnotations;
using UniDocuments.App.Shared.Documents;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Neural;

public class NeuralTrainViewModel
{
    public DocumentsGlobalData GlobalData { get; set; } = null!;
    
    [Range(4, int.MaxValue, ErrorMessage = "Размер вектора документов должен быть больше 4")]
    public int EmbeddingSize { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Количество эпох обучения должно быть больше 1")]
    public int Epochs { get; set; }
    [Required(ErrorMessage = "Скорость обучения не может быть пустой")]
    public string LearningRate { get; set; } = null!;
}