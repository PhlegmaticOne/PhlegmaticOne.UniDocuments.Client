using UniDocuments.ApiRequesting.Models;
using UniDocuments.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Activities.Created;
using UniDocuments.App.Shared.Shared;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Activities;

public class RequestGetActivitiesTeacher : ClientGetRequest<PagedListData, ActivityCreatedList>
{
    public RequestGetActivitiesTeacher(PagedListData requestData) : base(requestData) { }

    public override string BuildQueryString()
    {
        return WithManyQueryParameters(
            new GetRequestQueryParameter("pageIndex", RequestData.PageIndex),
            new GetRequestQueryParameter("pageSize", RequestData.PageSize));
    }
}