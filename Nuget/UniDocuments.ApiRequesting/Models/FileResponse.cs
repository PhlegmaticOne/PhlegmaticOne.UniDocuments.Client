using System.Net.Http.Headers;

namespace UniDocuments.ApiRequesting.Models;

public class FileResponse
{
    public Stream Stream { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public MediaTypeHeaderValue ContentType { get; set; } = null!;
}