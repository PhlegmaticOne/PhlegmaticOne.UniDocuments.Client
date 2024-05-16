namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Home;

public class NavigationCardViewModel
{
    public NavigationCardViewModel(string title, string actionName,  string controller, string action, string iconClasses)
    {
        Title = title;
        Action = action;
        ActionName = actionName;
        Controller = controller;
        IconClasses = iconClasses;
    }
    
    public string Title { get; set; }
    public string Action { get; set; }
    public string ActionName { get; set; }
    public string Controller { get; set; }
    public string IconClasses { get; set; }
}