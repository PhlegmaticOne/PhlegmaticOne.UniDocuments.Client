using Microsoft.AspNetCore.Http;

namespace UniDocuments.App.Shared.Documents;

public class DocumentDetailedCheckDocumentObject
{
    public IFormFile File { get; set; } = null!;
    public int TopCount { get; set; }
    public int InferEpochs { get; set; }
    public string ModelName { get; set; } = null!;
    public string BaseMetric { get; set; } = null!;
}