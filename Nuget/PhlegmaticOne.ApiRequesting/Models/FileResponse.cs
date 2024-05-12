using System.Net.Http.Headers;

namespace PhlegmaticOne.ApiRequesting.Models;

public class FileResponse
{
    public Stream Stream { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public MediaTypeHeaderValue ContentType { get; set; } = null!;
}