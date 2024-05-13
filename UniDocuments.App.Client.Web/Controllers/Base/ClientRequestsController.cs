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
using PhlegmaticOne.OperationResults;
using UniDocuments.App.Client.Web.Infrastructure.Extensions;
using UniDocuments.App.Client.Web.Infrastructure.Helpers;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Base;
using UniDocuments.App.Shared.Users;

namespace UniDocuments.App.Client.Web.Controllers.Base;

public class ClientRequestsController : Controller
{
    private const string Jwt = "jwt";
    
    private readonly IClientRequestsService _clientRequestsService;
    
    protected readonly IMapper Mapper;

    public ClientRequestsController(IClientRequestsService clientRequestsService, IMapper mapper)
    {
        Mapper = mapper;
        _clientRequestsService = clientRequestsService;
    }

    protected async Task<IActionResult> Get<TRequest, TResponse>(
        ClientGetRequest<TRequest, TResponse> clientGetRequest,
        Func<TResponse, Task<IActionResult>> onSuccess,
        Func<OperationResult, IActionResult>? onFailed = null)
    {
        var serverResponse = await _clientRequestsService.GetAsync(clientGetRequest, GetJwtToken());
        return await HandleResponseAsync(serverResponse, onSuccess, onFailed);
    }
    
    protected async Task<IActionResult> Get<TRequest, TResponse>(
        ClientGetRequest<TRequest, TResponse> clientGetRequest,
        Func<TResponse, IActionResult> onSuccess,
        Func<OperationResult, IActionResult>? onFailed = null)
    {
        var serverResponse = await _clientRequestsService.GetAsync(clientGetRequest, GetJwtToken());
        return await HandleResponse(serverResponse, onSuccess, onFailed);
    }

    protected async Task<IActionResult> Post<TRequest, TResponse>(
        ClientPostRequest<TRequest, TResponse> clientPostRequest,
        Func<TResponse, Task<IActionResult>> onSuccess,
        Func<OperationResult, IActionResult>? onFailed = null)
    {
        var serverResponse = await _clientRequestsService.PostAsync(clientPostRequest, GetJwtToken());
        return await HandleResponseAsync(serverResponse, onSuccess, onFailed);
    }
    
    protected async Task<IActionResult> Post<TRequest, TResponse>(
        ClientPostRequest<TRequest, TResponse> clientPostRequest,
        Func<TResponse, IActionResult> onSuccess,
        Func<OperationResult, IActionResult>? onFailed = null)
    {
        var serverResponse = await _clientRequestsService.PostAsync(clientPostRequest, GetJwtToken());
        return await HandleResponse(serverResponse, onSuccess, onFailed);
    }
    
    protected async Task<IActionResult> PostForm<TRequest, TResponse>(
        ClientFormDataRequest<TRequest, TResponse> clientPostRequest,
        Func<TResponse, IActionResult> onSuccess,
        Func<OperationResult, IActionResult>? onFailed = null)
    {
        var serverResponse = await _clientRequestsService.PostFormAsync(clientPostRequest, GetJwtToken());
        return await HandleResponse(serverResponse, onSuccess, onFailed);
    }

    protected async Task<IActionResult> Put<TRequest, TResponse>(
        ClientPutRequest<TRequest, TResponse> clientPostRequest,
        Func<TResponse, Task<IActionResult>> onSuccess,
        Func<OperationResult, IActionResult>? onFailed = null)
    {
        var serverResponse = await _clientRequestsService.PutAsync(clientPostRequest, GetJwtToken());
        return await HandleResponseAsync(serverResponse, onSuccess, onFailed);
    }
    
    protected async Task<IActionResult> DownloadFile<TRequest>(ClientGetFileRequest<TRequest> clientGetRequest)
    {
        var serverResponse = await _clientRequestsService.DownloadFileAsync(clientGetRequest, GetJwtToken());
        var fileData = serverResponse.GetData()!;
        return File(fileData.Stream, fileData.ContentType.MediaType!, fileData.FileName);
    }

    protected IActionResult HomeView()
    {
        return Redirect("/");
    }

    protected IActionResult ViewWithErrorsFromOperationResult(
        OperationResult operationResult, string viewName, ErrorHaving viewModel)
    {
        viewModel.ErrorMessage = operationResult.ErrorData;
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
        return SignInAsync(claimsPrincipal, profileObject.JwtToken);
    }

    protected Task SignOutAsync()
    {
        RemoveJwtToken();
        return HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    private Task SignInAsync(ClaimsPrincipal claimsPrincipal, JwtTokenObject jwtToken)
    {
        HttpContext.Response.Cookies.Append(Jwt, jwtToken.Token!);
        return HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
    }

    private string? GetJwtToken()
    {
        return HttpContext.Request.Cookies[Jwt];
    }

    private void RemoveJwtToken()
    {
        HttpContext.Response.Cookies.Delete(Jwt);
    }

    private IActionResult LoginView()
    {
        return Redirect("/Auth/Login");
    }

    private IActionResult ErrorView(string errorMessage)
    {
        return RedirectToAction("Error", "Home", new { errorMessage });
    }
    
    private async Task<IActionResult> HandleResponse<TResponse>(
        ServerResponse<TResponse> serverResponse,
        Func<TResponse, IActionResult> onSuccess,
        Func<OperationResult, IActionResult>? onFailed = null)
    {
        if (serverResponse.IsUnauthorized)
        {
            await SignOutAsync();
            return LoginView();
        }

        var operationResult = serverResponse.OperationResult!;
        
        if (serverResponse.IsSuccess == false)
        {
            return onFailed is not null
                ? onFailed(operationResult!)
                : ErrorView(operationResult.ErrorData!);
        }

        var data = serverResponse.GetData()!;
        return onSuccess(data);
    }

    private async Task<IActionResult> HandleResponseAsync<TResponse>(
        ServerResponse<TResponse> serverResponse,
        Func<TResponse, Task<IActionResult>> onSuccess,
        Func<OperationResult, IActionResult>? onFailed = null)
    {
        if (serverResponse.IsUnauthorized)
        {
            await SignOutAsync();
            return LoginView();
        }

        var operationResult = serverResponse.OperationResult!;
        
        if (serverResponse.IsSuccess == false)
        {
            return onFailed is not null
                ? onFailed(operationResult!)
                : ErrorView(operationResult.ErrorData!);
        }

        var data = serverResponse.GetData()!;
        return await onSuccess(data);
    }
}