using Microsoft.AspNetCore.Mvc.Rendering;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Base;
using UniDocuments.App.Shared.Admin;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Admin;

public class AdminTrainModelViewModel : ErrorHaving
{
    public NeuralModelTrainResult? TrainResult { get; set; }
    public string ModelName { get; set; } = null!;
    
    public List<SelectListItem> ModelNames { get; set; } = new()
    {
        new SelectListItem("doc2vec", "doc2vec"),
        new SelectListItem("keras", "keras2vec"),
    };
}