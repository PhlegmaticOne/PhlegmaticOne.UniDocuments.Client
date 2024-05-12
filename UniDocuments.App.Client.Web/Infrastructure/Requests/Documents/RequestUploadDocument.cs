using PhlegmaticOne.ApiRequesting.Models;
using PhlegmaticOne.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Documents;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Documents;

public class RequestUploadDocument : ClientFormDataRequest<DocumentUploadObject, Guid>
{
    public RequestUploadDocument(DocumentUploadObject requestData) : base(requestData) { }

    public override void FillFormData(IFormData formData)
    {
        formData.AddFile("File", RequestData.File);
        formData.AddValue("Id", RequestData.Id);
    }
}