using UniDocuments.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Activities.Create;
using UniDocuments.App.Shared.Activities.Detailed;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Activities;

public class RequestCreateActivity : ClientPostRequest<ActivityCreateObject, ActivityDetailedObject>
{
    public RequestCreateActivity(ActivityCreateObject requestData) : base(requestData) { }
}