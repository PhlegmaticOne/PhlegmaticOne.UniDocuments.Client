﻿using AutoMapper;
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
        IClientRequestsService clientRequestsService, 
        ILocalStorageService localStorageService,
        IMapper mapper,
        IValidator<RegisterViewModel> registerValidator) :
        base(clientRequestsService, localStorageService, mapper)
    {
        _registerValidator = registerValidator;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        var validationResult = await _registerValidator.ValidateAsync(registerViewModel);

        if (validationResult.IsValid == false)
        {
            return ViewWithErrorsFromValidationResult(validationResult, nameof(Register), registerViewModel);
        }

        var registerObject = Mapper.Map<RegisterObject>(registerViewModel);

        return await FromAuthorizedPost(new RegisterProfileRequest(registerObject), async profile =>
        {
            await AuthenticateAsync(profile);
            return RedirectToAction("Details", "Profile");
        }, result => ViewWithErrorsFromOperationResult(result, nameof(Register), registerViewModel));
    }

    [HttpPost]
    public Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        var loginObject = Mapper.Map<LoginObject>(loginViewModel);
        
        return FromAuthorizedPost(new LoginProfileRequest(loginObject), async profile =>
        {
            await AuthenticateAsync(profile);
            
            return loginViewModel.ReturnUrl is null
                ? RedirectToAction("Details", "Profile")
                : LocalRedirect(loginViewModel.ReturnUrl);
        }, result => ViewWithErrorsFromOperationResult(result, nameof(Login), loginViewModel));
    }
}