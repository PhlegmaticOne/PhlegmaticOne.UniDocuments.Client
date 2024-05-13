using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using PhlegmaticOne.ApiRequesting.Models;
using PhlegmaticOne.ApiRequesting.Models.Requests;
using PhlegmaticOne.ApiRequesting.Services;
using PhlegmaticOne.OperationResults;

namespace PhlegmaticOne.ApiRequesting.Implementation;

public class ClientRequestsService : IClientRequestsService
{
    private const string PreQueryPart = "/?";
    
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly Dictionary<Type, string> _requestUrls;
    private readonly string _httpClientName;

    public ClientRequestsService(IHttpClientFactory httpClientFactory, Dictionary<Type, string> requestUrls, string httpClientName)
    {
        _httpClientFactory = httpClientFactory;
        _httpClientName = httpClientName;
        _requestUrls = requestUrls;
    }

    public async Task<ServerResponse<TResponse>> PostAsync<TRequest, TResponse>(
        ClientPostRequest<TRequest, TResponse> postRequest, string? jwtToken = null)
    {
        var requestUrl = GetRequestUrl(postRequest);
        var httpClient = CreateHttpClientWithToken(jwtToken);

        try
        {
            var httpResponseMessage = await httpClient.PostAsJsonAsync(requestUrl, postRequest.RequestData);
            return await GetServerResponse<TResponse>(httpResponseMessage);
        }
        catch (HttpRequestException httpRequestException)
        {
            return ServerResponse.Error<TResponse>(httpRequestException.StatusCode, httpRequestException.Message);
        }
    }

    public async Task<ServerResponse<TResponse>> DeleteAsync<TRequest, TResponse>(
        ClientDeleteRequest<TRequest, TResponse> request, string? jwtToken = null)
    {
        var requestUrl = BuildRequestQuery(request);
        var httpClient = CreateHttpClientWithToken(jwtToken);

        try
        {
            var httpResponseMessage = await httpClient.DeleteAsync(requestUrl);
            return await GetServerResponse<TResponse>(httpResponseMessage);
        }
        catch (HttpRequestException httpRequestException)
        {
            return ServerResponse.Error<TResponse>(httpRequestException.StatusCode, httpRequestException.Message);
        }
    }

    public async Task<ServerResponse<TResponse>> PutAsync<TRequest, TResponse>(
        ClientPutRequest<TRequest, TResponse> request, string? jwtToken = null)
    {
        var requestUrl = GetRequestUrl(request);
        var httpClient = CreateHttpClientWithToken(jwtToken);

        try
        {
            var httpResponseMessage = await httpClient.PutAsJsonAsync(requestUrl, request.RequestData);
            return await GetServerResponse<TResponse>(httpResponseMessage);
        }
        catch (HttpRequestException httpRequestException)
        {
            return ServerResponse.Error<TResponse>(httpRequestException.StatusCode, httpRequestException.Message);
        }
    }

    public async Task<ServerResponse<TResponse>> GetAsync<TRequest, TResponse>(
        ClientGetRequest<TRequest, TResponse> getRequest, string? jwtToken = null)
    {
        var requestUrl = BuildGetQuery(getRequest);
        var httpClient = CreateHttpClientWithToken(jwtToken);

        try
        {
            var httpResponseMessage = await httpClient.GetAsync(requestUrl);
            return await GetServerResponse<TResponse>(httpResponseMessage);
        }
        catch (HttpRequestException httpRequestException)
        {
            return ServerResponse.Error<TResponse>(httpRequestException.StatusCode, httpRequestException.Message);
        }
    }

    public async Task<ServerResponse<FileResponse>> DownloadFileAsync<TRequest>(
        ClientGetFileRequest<TRequest> getRequest, string? jwtToken)
    {
        var requestUrl = BuildGetQuery(getRequest);
        var httpClient = CreateHttpClientWithToken(jwtToken);

        try
        {
            var httpResponseMessage = await httpClient.GetAsync(requestUrl);
            return await GetServerResponseFile(httpResponseMessage);
        }
        catch (HttpRequestException httpRequestException)
        {
            return ServerResponse.Error<FileResponse>(httpRequestException.StatusCode, httpRequestException.Message);
        }
    }

    public async Task<ServerResponse<TResponse>> PostFormAsync<TRequest, TResponse>(
        ClientFormDataRequest<TRequest, TResponse> request, string? jwtToken)
    {
        var requestUrl = GetRequestUrl(request);
        var httpClient = CreateHttpClientWithToken(jwtToken);

        try
        {
            var formData = new FormData();
            request.FillFormData(formData);
            var httpResponseMessage = await httpClient.PostAsync(requestUrl, formData.FormDataContent);
            return await GetServerResponse<TResponse>(httpResponseMessage);
        }
        catch (HttpRequestException httpRequestException)
        {
            return ServerResponse.Error<TResponse>(httpRequestException.StatusCode, httpRequestException.Message);
        }
    }

    private HttpClient CreateHttpClientWithToken(string? jwtToken)
    {
        var httpClient = _httpClientFactory.CreateClient(_httpClientName);
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(Constants.BearerAuthenticationSchemeName, jwtToken);
        return httpClient;
    }

    private string BuildRequestQuery<TRequest, TResponse>(ClientQueryBuildableRequest<TRequest, TResponse> request)
    {
        return string.Concat(GetRequestUrl(request), PreQueryPart, request.BuildQueryString());
    }

    private string BuildGetQuery<TRequest, TResponse>(ClientGetRequest<TRequest, TResponse> clientGetRequest)
    {
        return clientGetRequest.IsEmpty ? GetRequestUrl(clientGetRequest) : BuildRequestQuery(clientGetRequest);
    }

    private string GetRequestUrl(ClientRequest clientRequest)
    {
        return _requestUrls[clientRequest.GetType()];
    }

    private static async Task<ServerResponse<FileResponse>> GetServerResponseFile(HttpResponseMessage response)
    {
        var httpStatusCode = response.StatusCode;
        var reasonPhrase = response.ReasonPhrase;

        if (response.IsSuccessStatusCode == false)
        {
            return ServerResponse.Error<FileResponse>(httpStatusCode, reasonPhrase);
        }
        
        var operationResult = OperationResult.Successful(new FileResponse
        {
            Stream = await response.Content.ReadAsStreamAsync(),
            FileName = response.Content.Headers.ContentDisposition!.FileName!,
            ContentType = response.Content.Headers.ContentType!
        });
        
        return ServerResponse.Success(operationResult, httpStatusCode, reasonPhrase);
    }

    private static async Task<ServerResponse<TResponse>> GetServerResponse<TResponse>(HttpResponseMessage response)
    {
        var httpStatusCode = response.StatusCode;
        var reasonPhrase = response.ReasonPhrase;

        if (httpStatusCode == HttpStatusCode.Unauthorized)
        {
            return ServerResponse.Error<TResponse>(httpStatusCode, reasonPhrase);
        }

        try
        {
            var result = await response.Content.ReadFromJsonAsync<OperationResult<TResponse>>();
            
            return response.IsSuccessStatusCode == false ? 
                ServerResponse.Error(httpStatusCode, reasonPhrase, result) :
                ServerResponse.Success(result!, httpStatusCode, reasonPhrase);
        }
        catch
        {
            return ServerResponse.Error<TResponse>(httpStatusCode, reasonPhrase);
        }
    }
}