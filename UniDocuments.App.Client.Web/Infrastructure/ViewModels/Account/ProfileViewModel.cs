using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account.Base;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account;

public class ProfileViewModel : RoleViewsHaving
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public DateTime JoinDate { get; set; }
}