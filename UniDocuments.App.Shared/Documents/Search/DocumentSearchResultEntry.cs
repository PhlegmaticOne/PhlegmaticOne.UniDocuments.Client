using System.Globalization;

namespace UniDocuments.App.Shared.Documents.Search;

public class DocumentSearchResultEntry
{
    public Guid DocumentId { get; set; }
    public string DocumentName { get; set; } = null!;
    public DateTime DateLoaded { get; set; }
    public string StudentFirstName { get; set; } = null!;
    public string StudentLastName { get; set; } = null!;

    public string GetStudentInfo()
    {
        return $"{StudentFirstName} {StudentLastName}";
    }
    public string GetLoadedDate()
    {
        var time = DateLoaded.ToLocalTime();
        return time.ToString("f", CultureInfo.CurrentCulture);
    }
}