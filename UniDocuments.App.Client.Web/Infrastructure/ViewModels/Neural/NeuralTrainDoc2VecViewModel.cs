using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Neural;

public class NeuralTrainDoc2VecViewModel : NeuralTrainViewModel
{
    [Required(ErrorMessage = "MinAlpha не может быть пустой")]
    public string MinAlpha { get; set; } = null!;
    [Required(ErrorMessage = "Тип модели не может быть пустым")]
    public string Dm { get; set; } = null!;
    [Range(1, 30, ErrorMessage = "Количество потоков должно быть между 1 и 30")]
    public int WorkersCount { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Минимальное количество слов для учета должно быть больше 1")]
    public int MinWordsCount { get; set; }

    public List<SelectListItem> ModelTypes { get; set; } = new()
    {
        new SelectListItem("dbow", "0"),
        new SelectListItem("dm", "1")
    };
}