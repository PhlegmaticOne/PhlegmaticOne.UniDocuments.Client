using PhlegmaticOne.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Users;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Account;

public class UpdateProfileRequest : ClientPostRequest<UpdateProfileObject, ProfileObject>
{
    public UpdateProfileRequest(UpdateProfileObject requestData) : base(requestData)
    {
    }
}