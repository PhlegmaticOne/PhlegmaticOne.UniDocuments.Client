﻿using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniDocuments.ApiRequesting.Services;
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
    public IActionResult Details(ProfileViewModel viewModel)
    {
        return View(nameof(Details), viewModel);
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
        };

        return View(nameof(Update), viewModel);
    }

    [HttpPost]
    public Task<IActionResult> Update(UpdateAccountViewModel viewModel)
    {
        ValidateUpdateViewModel(viewModel);

        if (ModelState.IsValid == false)
        {
            return Task.FromResult<IActionResult>(View(viewModel));
        }

        var updateProfileObject = Mapper.Map<UpdateProfileObject>(viewModel);

        return Post(new RequestUpdateProfile(updateProfileObject), 
            async profile => 
            { 
                await SignOutAsync(); 
                await AuthenticateAsync(profile); 
                return RedirectToAction(nameof(Details), profile); 
            },
            _ =>
            {
                ModelState.AddModelError(nameof(UpdateAccountViewModel.OldPassword), "Введен некорректный старый пароль!");
                return View(viewModel);
            });
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await SignOutAsync();
        return HomeView();
    }

    private void ValidateUpdateViewModel(UpdateAccountViewModel viewModel)
    {
        var validationResult = _updateAccountViewModel.Validate(viewModel);

        if (validationResult.IsValid == false)
        {
            validationResult.AddToModelState(ModelState);
        }

        if (string.IsNullOrEmpty(viewModel.FirstName) &&
            string.IsNullOrEmpty(viewModel.LastName) &&
            string.IsNullOrEmpty(viewModel.NewPassword))
        {
            ModelState.AddModelError(
                nameof(UpdateAccountViewModel.OldPassword),
                "Для обновления информации измените имя, фамилию или пароль");
        }
    }
}