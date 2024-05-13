namespace PhlegmaticOne.ApiRequesting.Models.Requests;

public abstract class ClientGetFileRequest<TRequest> : ClientGetRequest<TRequest, FileResponse>
{
    protected ClientGetFileRequest(TRequest requestData) : base(requestData) { }
}