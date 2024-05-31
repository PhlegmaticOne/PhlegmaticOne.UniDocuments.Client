using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Document;

public class DocumentsSearchViewModel
{
    [Range(1, 15, ErrorMessage = "Количество документов должно быть между 1 и 15")]
    public int Count { get; set; }

    [Required(ErrorMessage = "Введите фразу для поиска")]
    public string Phrase { get; set; } = null!;

    public string ModelName { get; set; } = null!;
    
    public List<SelectListItem> ModelNames { get; set; } = new()
    {
        new SelectListItem("doc2vec", "doc2vec"),
        new SelectListItem("keras", "keras2vec"),
    }; 
}