using UniDocuments.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Neural;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Neural;

public class RequestRebuildDocuments : ClientPostRequest<object?, DocumentsUpdateObject>
{
    public RequestRebuildDocuments(object? requestData = null) : base(requestData)
    {
    }
}