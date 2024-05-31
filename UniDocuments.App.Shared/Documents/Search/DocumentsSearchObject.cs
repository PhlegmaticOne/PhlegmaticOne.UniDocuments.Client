namespace UniDocuments.App.Shared.Documents.Search;

public class DocumentsSearchObject
{
    public int Count { get; set; }
    public string ModelName { get; set; } = null!;
    public string Phrase { get; set; } = null!;
}