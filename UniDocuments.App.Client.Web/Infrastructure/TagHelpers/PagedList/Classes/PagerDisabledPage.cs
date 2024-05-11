using UniDocuments.App.Client.Web.Infrastructure.TagHelpers.PagedList.Classes.Base;

namespace UniDocuments.App.Client.Web.Infrastructure.TagHelpers.PagedList.Classes;

public class PagerDisabledPage : PagerPageBase
{
    public PagerDisabledPage(string title, int value) : base(title, value, false, true)
    {
    }
}