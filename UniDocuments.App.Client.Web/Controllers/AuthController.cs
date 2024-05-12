using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhlegmaticOne.ApiRequesting.Services;
using PhlegmaticOne.LocalStorage;
using UniDocuments.App.Client.Web.Controllers.Base;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Account;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account;
using UniDocuments.App.Shared.Users;

namespace UniDocuments.App.Client.Web.Controllers;

[AllowAnonymous]
public class AuthController : ClientRequestsController
{
    private readonly IValidator<RegisterViewModel> _registerValidator;

    public AuthController(
        IClientRequestsService clientRequestsService, IStorageService storageService, IMapper mapper,
        IValidator<RegisterViewModel> registerValidator) :
        base(clientRequestsService, storageService, mapper)
    {
        _registerValidator = registerValidator;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        var model = new LoginViewModel();
        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        var model = new RegisterViewModel();
        return View(model);
    }

    [HttpPost]
    public Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        var validationResult = _registerValidator.Validate(registerViewModel);

        if (validationResult.IsValid == false)
        {
            var view = ViewWithErrorsFromValidationResult(validationResult, nameof(Register), registerViewModel);
            return Task.FromResult(view);
        }

        var registerObject = Mapper.Map<RegisterObject>(registerViewModel);

        return Post(new RequestRegister(registerObject), 
            async profile => 
            { 
                await AuthenticateAsync(profile); 
                return RedirectToAction("Details", "Profile", profile); 
            }, 
            result => ViewWithErrorsFromOperationResult(result, nameof(Register), registerViewModel));
    }

    [HttpPost]
    public Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        var loginObject = Mapper.Map<LoginObject>(loginViewModel);
        
        return Post(new RequestLogin(loginObject), 
            async profile => 
            { 
                await AuthenticateAsync(profile);
                
                return loginViewModel.ReturnUrl is null 
                    ? RedirectToAction("Details", "Profile", profile) 
                    : LocalRedirect(loginViewModel.ReturnUrl); 
            }, 
            result => ViewWithErrorsFromOperationResult(result, nameof(Login), loginViewModel));
    }
}