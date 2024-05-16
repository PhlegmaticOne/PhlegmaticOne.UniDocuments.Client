using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Neural;

public class NeuralTrainDoc2VecViewModel : NeuralTrainViewModel
{
    [Required]
    public string MinAlpha { get; set; } = null!;
    [Required]
    public string Dm { get; set; } = null!;
    [Range(1, 30)]
    public int WorkersCount { get; set; }
    [Range(1, int.MaxValue)]
    public int MinWordsCount { get; set; }

    public List<SelectListItem> ModelTypes { get; set; } = new()
    {
        new SelectListItem("dbow", "0"),
        new SelectListItem("dm", "1")
    };
}