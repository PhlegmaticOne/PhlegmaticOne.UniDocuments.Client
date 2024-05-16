using System.Security.Claims;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Home;

namespace UniDocuments.App.Client.Web.Infrastructure.Services.Navigation;

public interface INavigationCreator
{
    List<NavigationCardViewModel> Create(ClaimsPrincipal user);
}