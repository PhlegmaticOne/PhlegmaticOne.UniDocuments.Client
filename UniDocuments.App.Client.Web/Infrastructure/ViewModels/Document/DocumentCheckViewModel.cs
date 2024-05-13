using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Base;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Document;

public class DocumentCheckViewModel : ErrorHaving
{
    public Guid DocumentId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime DateLoaded { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    [Range(1, 10)]
    public int TopCount { get; set; }
    
    [Range(1, 50)]
    public int InferEpochs { get; set; }
    public string BaseMetric { get; set; } = null!;
    public string ModelName { get; set; } = null!;

    public List<SelectListItem> BaseMetrics { get; set; } = new()
    {
        new SelectListItem("Косинусное расстояние", "cosine"),
        new SelectListItem("TS-SS", "ts-ss"),
        new SelectListItem("Печать", "fingerprint")
    };
    
    public List<SelectListItem> ModelNames { get; set; } = new()
    {
        new SelectListItem("doc2vec", "doc2vec"),
        new SelectListItem("keras", "keras2vec"),
    };
}