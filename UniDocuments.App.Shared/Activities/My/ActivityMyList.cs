using UniDocuments.Results;

namespace UniDocuments.App.Shared.Activities.My;

public class ActivityMyList
{
    public PagedList<ActivityMyObject> Activities { get; set; } = null!;

    public void SetPageData(int pageIndex, int pageSize)
    {
        foreach (var activity in Activities.Items)
        {
            activity.PageSize = pageSize;
            activity.PageIndex = pageIndex + 1;
        }
    }
}