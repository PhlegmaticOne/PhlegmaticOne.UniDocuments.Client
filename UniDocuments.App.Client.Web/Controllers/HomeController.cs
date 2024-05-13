using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels;

namespace UniDocuments.App.Client.Web.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult RestrictedPage()
    {
        return View();
    }

    public IActionResult Error(string errorMessage)
    {
        return View(new ErrorViewModel { ErrorMessage = errorMessage });
    }
}