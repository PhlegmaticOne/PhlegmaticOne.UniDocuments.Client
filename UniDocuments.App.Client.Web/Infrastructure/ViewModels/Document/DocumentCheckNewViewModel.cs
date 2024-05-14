using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Document.Base;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Document;

public class DocumentCheckNewViewModel : DocumentCheckViewModel
{
    public IFormFile File { get; set; } = null!;
}