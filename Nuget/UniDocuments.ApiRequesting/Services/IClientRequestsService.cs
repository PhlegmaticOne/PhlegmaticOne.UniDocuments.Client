using UniDocuments.ApiRequesting.Models;
using UniDocuments.ApiRequesting.Models.Requests;

namespace UniDocuments.ApiRequesting.Services;

public interface IClientRequestsService
{
    Task<ServerResponse<TResponse>> PostAsync<TRequest, TResponse>(
        ClientPostRequest<TRequest, TResponse> request, string? jwtToken = null);

    Task<ServerResponse<TResponse>> DeleteAsync<TRequest, TResponse>(
        ClientDeleteRequest<TRequest, TResponse> request, string? jwtToken = null);

    Task<ServerResponse<TResponse>> PutAsync<TRequest, TResponse>(
        ClientPutRequest<TRequest, TResponse> request, string? jwtToken = null);

    Task<ServerResponse<TResponse>> GetAsync<TRequest, TResponse>(
        ClientGetRequest<TRequest, TResponse> request, string? jwtToken = null);

    Task<ServerResponse<TResponse>> PostFormAsync<TRequest, TResponse>(
        ClientFormDataRequest<TRequest, TResponse> request, string? jwtToken);

    Task<ServerResponse<FileResponse>> DownloadFileAsync<TRequest>(
        ClientGetFileRequest<TRequest> request, string? jwtToken);

    Task<ServerResponse<FileResponse>> DownloadFileAsync<TRequest>(
        ClientFormDataRequest<TRequest, FileResponse> request, string? jwtToken);
}