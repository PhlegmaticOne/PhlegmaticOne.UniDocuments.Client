using Microsoft.AspNetCore.Mvc.Rendering;
using UniDocuments.App.Shared.Neural;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Neural;

public class NeuralTrainDoc2VecViewModel : NeuralTrainViewModel
{
    public NeuralTrainResultDoc2Vec? TrainResult { get; set; }
    public string MinAlpha { get; set; } = null!;
    public string Dm { get; set; } = null!;
    public int WorkersCount { get; set; }
    public int MinWordsCount { get; set; }

    public List<SelectListItem> ModelTypes { get; set; } = new()
    {
        new SelectListItem("dbow", "0"),
        new SelectListItem("dm", "1")
    };
}