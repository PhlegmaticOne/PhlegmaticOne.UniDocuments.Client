using UniDocuments.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Documents.Search;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Documents;

public class RequestSearchDocument : ClientPostRequest<DocumentsSearchObject, DocumentsSearchResult>
{
    public RequestSearchDocument(DocumentsSearchObject requestData) : base(requestData)
    {
    }
}