using System.ComponentModel.DataAnnotations;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Neural;

public class NeuralTrainKerasViewModel : NeuralTrainViewModel
{
    [Range(3, int.MaxValue, ErrorMessage = "Размер окна должен быть больше 3")]
    public int WindowSize { get; set; }
    
    [Range(3, int.MaxValue, ErrorMessage = "Размер батча должен быть больше 3")]
    public int BatchSize { get; set; }
}