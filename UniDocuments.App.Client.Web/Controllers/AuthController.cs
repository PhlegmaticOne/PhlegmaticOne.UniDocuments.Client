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
    public Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        ValidateRegister(registerViewModel);

        if (ModelState.IsValid == false)
        {
            return Task.FromResult<IActionResult>(View(registerViewModel));
        }

        var registerObject = Mapper.Map<RegisterObject>(registerViewModel);

        return Post(new RequestRegister(registerObject), async profile => 
        { 
            await AuthenticateAsync(profile); 
            return RedirectToAction("Details", "Profile", profile); 
        });
    }

    [HttpPost]
    public Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        var loginObject = Mapper.Map<LoginObject>(loginViewModel);
        
        if (ModelState.IsValid == false)
        {
            return Task.FromResult<IActionResult>(View(loginViewModel));
        }
        
        return Post(new RequestLogin(loginObject), 
            async profile => 
            { 
                await AuthenticateAsync(profile);
                
                return loginViewModel.ReturnUrl is null 
                    ? RedirectToAction("Details", "Profile", profile) 
                    : LocalRedirect(loginViewModel.ReturnUrl); 
            }, 
            _ =>
            {
                ModelState.AddModelError(nameof(LoginViewModel.UserName), ErrorMessageUserNotFound);
                return View(loginViewModel);
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