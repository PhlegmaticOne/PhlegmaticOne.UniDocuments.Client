using PhlegmaticOne.ApiRequesting.Models;
using PhlegmaticOne.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Activities.My;
using UniDocuments.App.Shared.Shared;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Activities;

public class RequestGetActivitiesStudent : ClientGetRequest<PagedListData, ActivityMyList>
{
    public RequestGetActivitiesStudent(PagedListData requestData) : base(requestData)
    {
    }

    public override string BuildQueryString()
    {
        return WithManyQueryParameters(
            new GetRequestQueryParameter("pageIndex", RequestData.PageIndex),
            new GetRequestQueryParameter("pageSize", RequestData.PageSize));
    }
}