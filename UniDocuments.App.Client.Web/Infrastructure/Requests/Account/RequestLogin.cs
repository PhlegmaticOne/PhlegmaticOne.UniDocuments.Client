using PhlegmaticOne.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Users;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Account;

public class RequestLogin : ClientPostRequest<LoginObject, ProfileObject>
{
    public RequestLogin(LoginObject requestData) : base(requestData)
    {
    }
}