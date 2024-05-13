namespace UniDocuments.App.Shared.Documents;

public class DocumentDetailedCheckObject
{
    public Guid DocumentId { get; set; }
    public int TopCount { get; set; }
    public int InferEpochs { get; set; }
    public string ModelName { get; set; } = null!;
    public string BaseMetric { get; set; } = null!;
}