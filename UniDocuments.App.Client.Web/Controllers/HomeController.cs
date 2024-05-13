using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels;

namespace UniDocuments.App.Client.Web.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
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