using UniDocuments.App.Client.Web.Infrastructure.TagHelpers.PagedList.Classes.Base;

namespace UniDocuments.App.Client.Web.Infrastructure.TagHelpers.PagedList.Classes;

public class PagerActivePage : PagerPageBase
{
    public PagerActivePage(string title, int value) : base(title, value, true)
    {
    }
}