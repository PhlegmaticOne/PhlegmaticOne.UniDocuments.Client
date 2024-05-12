namespace UniDocuments.App.Shared.Activities.Detailed;

public class ActivityDetailedStudentObject
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public ActivityDetailedDocumentObject? Document { get; set; }

    public bool HasLoadedDocument()
    {
        return Document is not null;
    }

    public string GetView()
    {
        return $"{FirstName} {LastName}";
    }
}