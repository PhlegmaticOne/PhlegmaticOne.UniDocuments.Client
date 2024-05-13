using PhlegmaticOne.ApiRequesting.Models;
using PhlegmaticOne.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Documents;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Documents;

public class RequestDetailedCheckDocument : ClientGetFileRequest<DocumentDetailedCheckObject>
{
    public RequestDetailedCheckDocument(DocumentDetailedCheckObject requestData) : base(requestData) { }

    public override string BuildQueryString()
    {
        return WithManyQueryParameters(
            new GetRequestQueryParameter("documentId", RequestData.DocumentId),
            new GetRequestQueryParameter("topCount", RequestData.TopCount),
            new GetRequestQueryParameter("inferEpochs", RequestData.InferEpochs),
            new GetRequestQueryParameter("modelName", RequestData.ModelName),
            new GetRequestQueryParameter("baseMetric", RequestData.BaseMetric));
    }
}