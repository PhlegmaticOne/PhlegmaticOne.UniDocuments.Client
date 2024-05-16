using UniDocuments.App.Shared.Shared;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Home;

public class HomeViewModel
{
    public StatisticsData StatisticsData { get; set; } = null!;
    public List<NavigationCardViewModel> Navigation { get; set; } = null!;
}