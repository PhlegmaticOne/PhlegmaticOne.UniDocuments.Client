﻿using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhlegmaticOne.ApiRequesting.Services;
using UniDocuments.App.Client.Web.Controllers.Base;
using UniDocuments.App.Client.Web.Infrastructure.Extensions;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Account;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account;
using UniDocuments.App.Shared.Users;

namespace UniDocuments.App.Client.Web.Controllers;

[Authorize]
public class ProfileController : ClientRequestsController
{
    private readonly IValidator<UpdateAccountViewModel> _updateAccountViewModel;

    public ProfileController(
        IClientRequestsService clientRequestsService, IMapper mapper,
        IValidator<UpdateAccountViewModel> updateAccountViewModel) :
        base(clientRequestsService, mapper)
    {
        _updateAccountViewModel = updateAccountViewModel;
    }

    [HttpGet]
    public IActionResult Details()
    {
        var profileObject = new ProfileViewModel
        {
            Id = User.Id(),
            FirstName = User.Firstname(),
            LastName = User.Lastname(),
            UserName = User.Username(),
            Role = User.StudyRole(),
            AppRole = User.AppRole(),
            JoinDate = User.JoinDate()
        };

        return Details(profileObject);
    }

    [HttpPost]
    public IActionResult Details(ProfileViewModel profileObject)
    {
        return View(nameof(Details), profileObject);
    }
    
    [HttpGet]
    public IActionResult Update()
    {
        var viewModel = new UpdateAccountViewModel
        {
            OldFirstName = User.Firstname(),
            OldLastName = User.Lastname(),
            OldUserName = User.Username(),
            AppRole = User.AppRole(),
            Role = User.StudyRole(),
            JoinDate = User.JoinDate()
        };

        return View(nameof(Update), viewModel);
    }

    [HttpPost]
    public Task<IActionResult> Update(UpdateAccountViewModel updateAccountViewModel)
    {
        var validationResult = _updateAccountViewModel.Validate(updateAccountViewModel);

        if (validationResult.IsValid == false)
        {
            var view = ViewWithErrorsFromValidationResult(validationResult, nameof(Update), updateAccountViewModel);
            return Task.FromResult(view);
        }

        var updateProfileObject = Mapper.Map<UpdateProfileObject>(updateAccountViewModel);

        return Post(new RequestUpdateProfile(updateProfileObject), 
            async profile => 
            { 
                await SignOutAsync(); 
                await AuthenticateAsync(profile); 
                return RedirectToAction(nameof(Details), profile); 
            },
            result => ViewWithErrorsFromOperationResult(result, nameof(Update), updateAccountViewModel));
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await SignOutAsync();
        return HomeView();
    }
}