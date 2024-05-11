using PhlegmaticOne.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Users;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Account;

public class RequestUpdateProfile : ClientPostRequest<UpdateProfileObject, ProfileObject>
{
    public RequestUpdateProfile(UpdateProfileObject requestData) : base(requestData)
    {
    }
}