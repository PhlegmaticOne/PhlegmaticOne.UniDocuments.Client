using Newtonsoft.Json;

namespace UniDocuments.Results;

[Serializable]
public class OperationResult
{
    public bool IsSuccess { get; init; }
    public string? ErrorData { get; init; }
    public string? ErrorCode { get; init; }

    public virtual object? GetResult()
    {
        return null;
    }

    public T? GetErrorDataAs<T>()
    {
        return ErrorData is null ? default : JsonConvert.DeserializeObject<T>(ErrorData);
    }

    public static OperationResult<T> Successful<T>(T result)
    {
        return new OperationResult<T>
        {
            IsSuccess = true,
            ErrorData = null,
            Result = result
        };
    }
}

[Serializable]
public class OperationResult<T> : OperationResult
{
    public T? Result { get; init; }

    public override object? GetResult()
    {
        return Result;
    }
}