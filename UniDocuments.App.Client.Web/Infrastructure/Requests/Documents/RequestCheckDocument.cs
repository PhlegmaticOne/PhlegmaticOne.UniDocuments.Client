using PhlegmaticOne.ApiRequesting.Models;
using PhlegmaticOne.ApiRequesting.Models.Requests;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Documents;

public class RequestCheckDocument : ClientGetFileRequest<Guid>
{
    public RequestCheckDocument(Guid requestData) : base(requestData) { }

    public override string BuildQueryString()
    {
        return WithOneQueryParameter(new GetRequestQueryParameter("documentId", RequestData));
    }
}