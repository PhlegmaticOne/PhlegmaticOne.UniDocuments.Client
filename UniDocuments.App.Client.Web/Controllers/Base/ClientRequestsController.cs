using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using UniDocuments.ApiRequesting.Models;
using UniDocuments.ApiRequesting.Models.Requests;
using UniDocuments.ApiRequesting.Services;
using UniDocuments.App.Client.Web.Infrastructure.Helpers;
using UniDocuments.App.Shared.Users;
using UniDocuments.Results;

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
    
    protected async Task<IActionResult> DownloadFile<TRequest>(
        ClientGetFileRequest<TRequest> clientGetRequest, 
        Func<OperationResult, IActionResult>? onFailed = null)
    {
        var serverResponse = await _clientRequestsService.DownloadFileAsync(clientGetRequest, GetJwtToken());

        return await HandleResponse(serverResponse, 
            fileData => File(fileData.Stream, fileData.ContentType.MediaType!, fileData.FileName), 
            onFailed);
    }
    
    protected async Task<IActionResult> DownloadFile<TRequest>(
        ClientFormDataRequest<TRequest, FileResponse> clientGetRequest, 
        Func<OperationResult, IActionResult>? onFailed = null)
    {
        var serverResponse = await _clientRequestsService.DownloadFileAsync(clientGetRequest, GetJwtToken());
        return await HandleResponse(serverResponse, 
            fileData => File(fileData.Stream, fileData.ContentType.MediaType!, fileData.FileName), 
            onFailed);
    }

    protected IActionResult HomeView()
    {
        return Redirect("/");
    }

    protected IActionResult ErrorView(string errorMessage)
    {
        return RedirectToAction("Error", "Home", new { errorMessage });
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
            if (onFailed is not null)
            {
                return onFailed(operationResult);
            }

            if (operationResult?.ErrorData is not null)
            {
                return ErrorView(operationResult.ErrorData);
            }

            return RedirectToAction("ServerShutdown", "Home");
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
            if (onFailed is not null)
            {
                return onFailed(operationResult);
            }

            if (operationResult?.ErrorData is not null)
            {
                return ErrorView(operationResult.ErrorData);
            }

            return RedirectToAction("ServerShutdown", "Home");
        }

        var data = serverResponse.GetData()!;
        return await onSuccess(data);
    }
}