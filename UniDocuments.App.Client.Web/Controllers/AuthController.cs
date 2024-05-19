using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniDocuments.ApiRequesting.Services;
using UniDocuments.App.Client.Web.Controllers.Base;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Account;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account;
using UniDocuments.App.Shared.Users;

namespace UniDocuments.App.Client.Web.Controllers;

[AllowAnonymous]
public class AuthController : ClientRequestsController
{
    private const string ErrorMessageUserNotFound = "Пользователь не найден!";
    
    private readonly IValidator<RegisterViewModel> _registerValidator;

    public AuthController(
        IClientRequestsService clientRequestsService, IMapper mapper,
        IValidator<RegisterViewModel> registerValidator) :
        base(clientRequestsService, mapper)
    {
        _registerValidator = registerValidator;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel());
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }

    [HttpPost]
    public Task<IActionResult> Register(RegisterViewModel viewModel)
    {
        ValidateRegister(viewModel);

        if (ModelState.IsValid == false)
        {
            return Task.FromResult<IActionResult>(View(viewModel));
        }

        var registerObject = Mapper.Map<RegisterObject>(viewModel);

        return Post(new RequestRegister(registerObject),
            async profile => 
            { 
                await AuthenticateAsync(profile); 
                return RedirectToAction("Details", "Profile", profile); 
            },
            _ =>
            {
                ModelState.AddModelError(nameof(RegisterViewModel.UserName), "Пользователь с таким ником уже существует!");
                return View(viewModel);
            });
    }

    [HttpPost]
    public Task<IActionResult> Login(LoginViewModel viewModel)
    {
        var loginObject = Mapper.Map<LoginObject>(viewModel);
        
        if (ModelState.IsValid == false)
        {
            return Task.FromResult<IActionResult>(View(viewModel));
        }
        
        return Post(new RequestLogin(loginObject), 
            async profile => 
            { 
                await AuthenticateAsync(profile);
                
                return viewModel.ReturnUrl is null 
                    ? RedirectToAction("Details", "Profile", profile) 
                    : LocalRedirect(viewModel.ReturnUrl); 
            }, 
            _ =>
            {
                ModelState.AddModelError(nameof(LoginViewModel.UserName), ErrorMessageUserNotFound);
                return View(viewModel);
            });
    }

    private void ValidateRegister(RegisterViewModel viewModel)
    {
        var validationResult = _registerValidator.Validate(viewModel);

        if (validationResult.IsValid == false)
        {
            validationResult.AddToModelState(ModelState);
        }
    }
}