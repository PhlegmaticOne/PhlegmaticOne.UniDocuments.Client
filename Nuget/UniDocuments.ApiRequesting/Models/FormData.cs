using Microsoft.AspNetCore.Http;

namespace UniDocuments.ApiRequesting.Models;

public class FormData : IFormData
{
    public FormData()
    {
        FormDataContent = new MultipartFormDataContent();
    }

    public MultipartFormDataContent FormDataContent { get; }

    public void AddFile(string key, IFormFile file)
    {
        var content = new StreamContent(file.OpenReadStream());
        FormDataContent.Add(content, key, file.FileName);
    }

    public void AddString(string key, string value)
    {
        var content = new StringContent(value);
        FormDataContent.Add(content, key);
    }

    public void AddValue<T>(string key, T value)
    {
        AddString(key, value!.ToString()!);
    }
}