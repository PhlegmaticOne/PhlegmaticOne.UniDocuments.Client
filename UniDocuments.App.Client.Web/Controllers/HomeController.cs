using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhlegmaticOne.ApiRequesting.Services;
using UniDocuments.App.Client.Web.Controllers.Base;
using UniDocuments.App.Client.Web.Infrastructure.Requests;
using UniDocuments.App.Client.Web.Infrastructure.Services.Navigation;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Home;

namespace UniDocuments.App.Client.Web.Controllers;

[AllowAnonymous]
public class HomeController : ClientRequestsController
{
    private readonly INavigationCreator _navigationCreator;

    public HomeController(
        IClientRequestsService clientRequestsService, IMapper mapper, INavigationCreator navigationCreator) :
        base(clientRequestsService, mapper)
    {
        _navigationCreator = navigationCreator;
    }

    [HttpGet]
    public Task<IActionResult> Index()
    {
        return Get(new RequestGetStatistics(), result =>
        {
            return View(new HomeViewModel
            {
                Navigation = _navigationCreator.Create(User),
                StatisticsData = result
            });
        });
    }

    [HttpGet]
    public IActionResult RestrictedPage()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Error(string errorMessage)
    {
        return View(new ErrorViewModel { ErrorMessage = errorMessage });
    }
}