using System.Security.Claims;
using AutoMapper;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PhlegmaticOne.ApiRequesting.Models;
using PhlegmaticOne.ApiRequesting.Models.Requests;
using PhlegmaticOne.ApiRequesting.Services;
using PhlegmaticOne.LocalStorage;
using PhlegmaticOne.OperationResults;
using UniDocuments.App.Client.Web.Infrastructure.Extensions;
using UniDocuments.App.Client.Web.Infrastructure.Helpers;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Base;
using UniDocuments.App.Shared.Users;

namespace UniDocuments.App.Client.Web.Controllers.Base;

public class ClientRequestsController : Controller
{
    protected readonly IMapper Mapper;
    
    private readonly IClientRequestsService _clientRequestsService;
    private readonly IStorageService _storageService;

    public ClientRequestsController(
        IClientRequestsService clientRequestsService, 
        IStorageService storageService,
        IMapper mapper)
    {
        Mapper = mapper;
        _storageService = storageService;
        _clientRequestsService = clientRequestsService;
    }

    protected async Task<IActionResult> AuthorizedGet<TRequest, TResponse>(
        ClientGetRequest<TRequest, TResponse> clientGetRequest,
        Func<TResponse, Task<IActionResult>> onSuccess,
        Func<OperationResult, IActionResult>? onOperationFailed = null)
    {
        var serverResponse = await _clientRequestsService.GetAsync(clientGetRequest, JwtToken());
        return await HandleResponse(serverResponse, onSuccess, onOperationFailed);
    }

    protected async Task<IActionResult> AuthorizedPost<TRequest, TResponse>(
        ClientPostRequest<TRequest, TResponse> clientPostRequest,
        Func<TResponse, Task<IActionResult>> onSuccess,
        Func<OperationResult, IActionResult>? onOperationFailed = null)
    {
        var serverResponse = await _clientRequestsService.PostAsync(clientPostRequest, JwtToken());
        return await HandleResponse(serverResponse, onSuccess, onOperationFailed);
    }

    protected async Task<IActionResult> AuthorizedPut<TRequest, TResponse>(
        ClientPutRequest<TRequest, TResponse> clientPostRequest,
        Func<TResponse, Task<IActionResult>> onSuccess,
        Func<OperationResult, IActionResult>? onOperationFailed = null)
    {
        var serverResponse = await _clientRequestsService.PutAsync(clientPostRequest, JwtToken());
        return await HandleResponse(serverResponse, onSuccess, onOperationFailed);
    }

    protected IActionResult LoginView()
    {
        return Redirect("/Auth/Login");
    }

    protected IActionResult ErrorView(string errorMessage)
    {
        return RedirectToAction("Error", "Home", new { errorMessage });
    }

    protected IActionResult HomeView()
    {
        return Redirect("/");
    }

    protected IActionResult ViewWithErrorsFromOperationResult(
        OperationResult operationResult, string viewName, ErrorHaving viewModel)
    {
        viewModel.ErrorMessage = operationResult.ErrorMessage;
        return View(viewName, viewModel);
    }

    protected IActionResult ViewWithErrorsFromValidationResult(
        ValidationResult validationResult, string viewName, ErrorHaving viewModel)
    {
        validationResult.AddToModelState(ModelState);
        return View(viewName, viewModel);
    }

    protected Task AuthenticateAsync(ProfileObject profileObject)
    {
        var claimsPrincipal = ClaimsPrincipalGenerator.GenerateClaimsPrincipal(profileObject);
        return SignInAsync(profileObject.Id, claimsPrincipal, profileObject.JwtToken);
    }

    protected Task SignOutAsync()
    {
        SetJwtToken(User.Id(), JwtTokenObject.Empty);
        return HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    private Task SignInAsync(Guid id, ClaimsPrincipal claimsPrincipal, JwtTokenObject jwtToken)
    {
        SetJwtToken(id, jwtToken);
        return HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
    }

    private string? JwtToken()
    {
        var id = User.IdString();

        if (id is null)
        {
            return null;
        }
        
        var token = _storageService.GetValue<JwtTokenObject>(id);
        return token?.Token;
    }

    private void SetJwtToken(Guid id, JwtTokenObject jwtToken)
    {
        _storageService.SetValue(id.ToString(), jwtToken, TimeSpan.FromMinutes(jwtToken.ExpirationInMinutes));
    }

    private async Task<IActionResult> HandleResponse<TResponse>(
        ServerResponse<TResponse> serverResponse,
        Func<TResponse, Task<IActionResult>> onSuccess,
        Func<OperationResult, IActionResult>? onOperationFailed = null)
    {
        if (serverResponse.IsUnauthorized)
        {
            await SignOutAsync();
            return LoginView();
        }

        var operationResult = serverResponse.OperationResult!;
        
        if (serverResponse.IsSuccess == false)
        {
            return onOperationFailed is not null
                ? onOperationFailed(operationResult!)
                : ErrorView(operationResult.ErrorMessage!);
        }

        var data = serverResponse.GetData()!;
        return await onSuccess(data);
    }
}