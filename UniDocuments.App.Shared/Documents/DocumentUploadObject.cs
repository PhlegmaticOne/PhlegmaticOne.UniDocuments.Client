using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace UniDocuments.App.Shared.Documents;

public class DocumentUploadObject
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Выберите документ для загрузки")]
    public IFormFile File { get; set; } = null!;
    
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
}