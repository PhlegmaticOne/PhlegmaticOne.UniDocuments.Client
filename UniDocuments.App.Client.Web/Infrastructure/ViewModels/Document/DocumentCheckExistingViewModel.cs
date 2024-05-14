using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Document.Base;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Document;

public class DocumentCheckExistingViewModel : DocumentCheckViewModel
{
    public Guid DocumentId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime DateLoaded { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}