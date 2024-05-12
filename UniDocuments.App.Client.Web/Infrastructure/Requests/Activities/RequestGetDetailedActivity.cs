using PhlegmaticOne.ApiRequesting.Models;
using PhlegmaticOne.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Activities.Detailed;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Activities;

public class RequestGetDetailedActivity : ClientGetRequest<Guid, ActivityDetailedObject>
{
    public RequestGetDetailedActivity(Guid requestData) : base(requestData) { }

    public override string BuildQueryString()
    {
        return WithOneQueryParameter(new GetRequestQueryParameter("activityId", RequestData));
    }
}