using UniDocuments.ApiRequesting.Models.Requests;
using UniDocuments.App.Shared.Admin;

namespace UniDocuments.App.Client.Web.Infrastructure.Requests.Admin;

public class RequestMakeAdmin : ClientPostRequest<AdminCreateObject, bool>
{
    public RequestMakeAdmin(AdminCreateObject requestData) : base(requestData) { }
}