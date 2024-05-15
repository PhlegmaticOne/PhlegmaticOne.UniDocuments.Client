using PhlegmaticOne.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Neural;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Neural;

public class RequestTrainDoc2Vec : ClientPostRequest<NeuralTrainOptionsDoc2Vec, NeuralTrainResultDoc2Vec>
{
    public RequestTrainDoc2Vec(NeuralTrainOptionsDoc2Vec requestData) : base(requestData) { }
}