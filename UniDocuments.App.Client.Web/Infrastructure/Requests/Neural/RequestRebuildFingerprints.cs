using UniDocuments.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Neural;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Neural;

public class RequestRebuildFingerprints : ClientPostRequest<object?, FingerprintsUpdateObject>
{
    public RequestRebuildFingerprints(object? requestData = null) : base(requestData)
    {
    }
}