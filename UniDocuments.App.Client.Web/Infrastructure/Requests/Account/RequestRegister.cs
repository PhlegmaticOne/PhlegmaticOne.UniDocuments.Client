using PhlegmaticOne.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Users;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Account;

public class RequestRegister : ClientPostRequest<RegisterObject, ProfileObject>
{
    public RequestRegister(RegisterObject requestData) : base(requestData)
    {
    }
}