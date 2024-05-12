namespace PhlegmaticOne.ApiRequesting.Models.Requests;

public abstract class ClientFormDataRequest<TRequest, TResponse> : ClientPostRequest<TRequest, TResponse>
{
    protected ClientFormDataRequest(TRequest requestData) : base(requestData) { }

    public abstract void FillFormData(IFormData formData);
}