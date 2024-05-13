using PhlegmaticOne.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Admin;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Admin;

public class RequestTrainModel : ClientPostRequest<AdminTrainModelObject, NeuralModelTrainResult>
{
    public RequestTrainModel(AdminTrainModelObject requestData) : base(requestData) { }
}