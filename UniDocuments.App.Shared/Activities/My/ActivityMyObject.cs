using UniDocuments.App.Shared.Activities.Shared;

namespace UniDocuments.App.Shared.Activities.My;

public class ActivityMyObject : IHaveActivityTime
{
    public Guid Id { get; set; }
    public string CreatorFirstName { get; set; } = null!;
    public string CreatorLastName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ActivityDocumentObject? Document { get; set; }
    
    public int PageSize { get; set; }
    public int PageIndex { get; set; }

    public ActivityState GetActivityState()
    {
        var now = DateTime.UtcNow;

        if (now < StartDate)
        {
            return ActivityState.Pending;
        }

        if (now >= StartDate && now <= EndDate)
        {
            return ActivityState.Active;
        }

        return ActivityState.Ended;
    }

    public bool IsExpired()
    {
        return DateTime.UtcNow > EndDate;
    }

    public bool HasLoadedDocument()
    {
        return Document is not null;
    }

    public string GetTitle()
    {
        return $"{CreatorFirstName} {CreatorLastName}";
    }

    public string GetTerms()
    {
        return $"{StartDate.ToLocalTime():MM/dd/yyyy} - {EndDate.ToLocalTime():MM/dd/yyyy}";
    }

    public string GetPendingTime()
    {
        return FormatTime(StartDate.ToLocalTime() - DateTime.Now);
    }

    public string GetEndTimeForward()
    {
        return FormatTime(EndDate.ToLocalTime() - DateTime.Now);
    }

    public string GetEndTimeBack()
    {
        return FormatTime(DateTime.Now - EndDate.ToLocalTime());
    }
    
    private static string FormatTime(TimeSpan timeSpan)
    {
        return $"{timeSpan.Days} д {timeSpan.Hours} ч {timeSpan.Minutes} м";
    }
}