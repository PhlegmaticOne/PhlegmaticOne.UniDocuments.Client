using UniDocuments.ApiRequesting.Models;
using UniDocuments.ApiRequesting.Models.Requests;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Documents;

public class RequestDefaultCheckDocument : ClientGetFileRequest<Guid>
{
    public RequestDefaultCheckDocument(Guid requestData) : base(requestData) { }

    public override string BuildQueryString()
    {
        return WithOneQueryParameter(
            new GetRequestQueryParameter("documentId", RequestData));
    }
}