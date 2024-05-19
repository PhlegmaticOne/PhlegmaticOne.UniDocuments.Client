using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Base;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Document.Base;

public class DocumentCheckViewModel : ErrorHaving
{
    [Range(1, 10, ErrorMessage = "Число параграфом должно быть между 1 и 10")]
    public int TopCount { get; set; }
    
    [Range(1, 50, ErrorMessage = "Число эпох должно быть между 1 и 50")]
    public int InferEpochs { get; set; }
    [Required(ErrorMessage = "Метрика не может быть пустой")]
    public string BaseMetric { get; set; } = null!;
    [Required(ErrorMessage = "Имя модели не может быть пустым")]
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