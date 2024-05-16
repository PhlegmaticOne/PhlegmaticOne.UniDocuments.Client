namespace UniDocuments.App.Shared.Shared;

public class StatisticsData
{
    public int DocumentsLoaded { get; set; }
    public int StudentsCount { get; set; }
    public int TeachersCount { get; set; }
    public int ActivitiesCount { get; set; }
    public TimeSpan PlatformActiveTime { get; set; }

    public string GetPlatformActiveTime()
    {
        return FormatTime(PlatformActiveTime);
    }
    
    private static string FormatTime(TimeSpan timeSpan)
    {
        return $"{timeSpan.Days} д {timeSpan.Hours} ч {timeSpan.Minutes} м";
    }
}