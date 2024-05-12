using System.Globalization;

namespace UniDocuments.App.Shared.Activities.Detailed;

public class ActivityDetailedObject
{
    public Guid Id { get; set; }
    public string CreatorFirstName { get; set; } = null!;
    public string CreatorLastName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreationDate { get; set; }
    public List<ActivityDetailedStudentObject> Students { get; set; } = new();

    public string GetTitle()
    {
        return $"{CreatorFirstName} {CreatorLastName}";
    }
    
    public string GetTerms()
    {
        return $"{StartDate.ToLocalTime():MM/dd/yyyy} - {EndDate.ToLocalTime():MM/dd/yyyy}";
    }
    
    public string GetCreationDate()
    {
        var time = CreationDate.ToLocalTime();
        return time.ToString("f", CultureInfo.CurrentCulture);
    }
    
    public bool IsStarted()
    {
        return DateTime.UtcNow > StartDate;
    }
    
    public bool IsEnd()
    {
        return DateTime.UtcNow > EndDate;
    }
    
    public string GetLastTime()
    {
        return FormatTime(DateTime.Now - StartDate.ToLocalTime());
    }

    public string GetRemainTime()
    {
        return FormatTime(EndDate.ToLocalTime() - DateTime.Now);
    }

    private static string FormatTime(TimeSpan timeSpan)
    {
        return $"{timeSpan.Days} д {timeSpan.Hours} ч {timeSpan.Minutes} м";
    }
}