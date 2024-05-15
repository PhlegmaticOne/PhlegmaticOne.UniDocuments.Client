using PhlegmaticOne.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Admin;
using UniDocuments.App.Shared.Neural;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Neural;

public class RequestTrainKeras : ClientPostRequest<NeuralTrainOptionsKeras, NeuralTrainResultKeras>
{
    public RequestTrainKeras(NeuralTrainOptionsKeras requestData) : base(requestData) { }
}