namespace PhlegmaticOne.LocalStorage;

public interface IStorageService
{
    void SetValue<T>(string key, T value, TimeSpan time);
    T? GetValue<T>(string key);
}