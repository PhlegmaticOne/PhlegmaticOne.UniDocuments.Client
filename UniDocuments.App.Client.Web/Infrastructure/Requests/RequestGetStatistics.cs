using UniDocuments.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Shared;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests;

public class RequestGetStatistics : ClientGetRequest<object?, StatisticsData>
{
    public RequestGetStatistics(object? requestData = null) : base(requestData) { }

    public override string BuildQueryString()
    {
        return string.Empty;
    }
}