namespace UniDocuments.App.Shared.Activities.Shared;

public interface IHaveActivityTime : IHaveActivityStatus
{
    string GetPendingTime();
    string GetEndTimeForward();
    string GetEndTimeBack();
}