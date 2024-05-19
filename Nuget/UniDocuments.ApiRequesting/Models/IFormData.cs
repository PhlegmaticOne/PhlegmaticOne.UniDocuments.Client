using Microsoft.AspNetCore.Http;

namespace UniDocuments.ApiRequesting.Models;

public interface IFormData
{
    void AddFile(string key, IFormFile file);
    void AddString(string key, string value);
    void AddValue<T>(string key, T value);
}