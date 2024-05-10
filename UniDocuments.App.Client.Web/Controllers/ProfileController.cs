using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PhlegmaticOne.ApiRequesting.Services;
using PhlegmaticOne.LocalStorage;
using UniDocuments.App.Client.Web.Controllers.Base;
using UniDocuments.App.Client.Web.Infrastructure.Extensions;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Account;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account;
using UniDocuments.App.Shared.Users;

namespace UniDocuments.App.Client.Web.Controllers;

public class ProfileController : ClientRequestsController
{
    private readonly IValidator<UpdateAccountViewModel> _updateAccountViewModel;

    public ProfileController(IClientRequestsService clientRequestsService,
        ILocalStorageService localStorageService, IMapper mapper,
        IValidator<UpdateAccountViewModel> updateAccountViewModel) :
        base(clientRequestsService, localStorageService, mapper)
    {
        _updateAccountViewModel = updateAccountViewModel;
    }

    [HttpGet]
    public IActionResult Details()
    {
        var profileViewModel = new ProfileViewModel
        {
            FirstName = User.Firstname(),
            Role = User.Role(),
            LastName = User.Lastname(),
            UserName = User.Username()
        };

        return View(nameof(Details), profileViewModel);
    }
    
    [HttpGet]
    public IActionResult Update()
    {
        var viewModel = new UpdateAccountViewModel
        {
            OldFirstName = User.Firstname(),
            OldLastName = User.Lastname(),
            OldUserName = User.Username()
        };

        return View(nameof(Update), viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateAccountViewModel updateAccountViewModel)
    {
        var validationResult = await _updateAccountViewModel.ValidateAsync(updateAccountViewModel);

        if (validationResult.IsValid == false)
        {
            return ViewWithErrorsFromValidationResult(validationResult, nameof(Update), updateAccountViewModel);
        }

        var updateProfileObject = Mapper.Map<UpdateProfileObject>(updateAccountViewModel);

        return await FromAuthorizedPost(new UpdateProfileRequest(updateProfileObject), async profile =>
        {
            await SignOutAsync();
            await AuthenticateAsync(profile);
            return RedirectToAction(nameof(Details));
        }, result => ViewWithErrorsFromOperationResult(result, nameof(Update), updateAccountViewModel));
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await SignOutAsync();
        return HomeView();
    }
}