using PhlegmaticOne.ApiRequesting.Models;
using PhlegmaticOne.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Activities.Display;
using UniDocuments.App.Shared.Shared;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Activities;

public class RequestGetActivitiesTeacher : ClientGetRequest<PagedListData, ActivityDisplayList>
{
    public RequestGetActivitiesTeacher(PagedListData requestData) : base(requestData) { }

    public override string BuildQueryString()
    {
        return WithManyQueryParameters(
            new GetRequestQueryParameter("pageIndex", RequestData.PageIndex),
            new GetRequestQueryParameter("pageSize", RequestData.PageSize));
    }
}