using PhlegmaticOne.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Documents;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Documents;

public class RequestGetGlobalData : ClientGetRequest<object?, DocumentsGlobalData>
{
    public RequestGetGlobalData(object? requestData = null) : base(requestData) { }

    public override string BuildQueryString()
    {
        return string.Empty;
    }
}