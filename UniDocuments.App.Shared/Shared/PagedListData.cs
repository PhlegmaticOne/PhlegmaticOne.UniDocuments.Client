namespace UniDocuments.App.Shared.Shared;

public class PagedListData
{
    public static PagedListData FromQueryData(int? pageIndex, int? pageSize)
    {
        return new PagedListData
        {
            PageIndex = pageIndex is null ? 0 : pageIndex.Value - 1,
            PageSize = pageSize ?? 15
        };
    }
    
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}