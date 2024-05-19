using UniDocuments.ApiRequesting.Models;
using UniDocuments.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Documents;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Documents;

public class RequestDetailedCheckDocumentNew : ClientFormDataRequest<DocumentDetailedCheckDocumentObject, FileResponse>
{
    public RequestDetailedCheckDocumentNew(DocumentDetailedCheckDocumentObject requestData) : base(requestData) { }

    public override void FillFormData(IFormData formData)
    {
        formData.AddFile("File", RequestData.File);
        formData.AddString("BaseMetric", RequestData.BaseMetric);
        formData.AddString("ModelName", RequestData.ModelName);
        formData.AddValue("InferEpochs", RequestData.InferEpochs);
        formData.AddValue("TopCount", RequestData.TopCount);
    }
}