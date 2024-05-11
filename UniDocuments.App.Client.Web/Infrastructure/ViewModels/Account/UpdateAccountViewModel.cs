using System.ComponentModel.DataAnnotations;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account.Base;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account;

public class UpdateAccountViewModel : RoleViewsHaving
{
    public string OldFirstName { get; set; } = null!;
    public string? FirstName { get; set; }
    public string OldLastName { get; set; } = null!;
    public string? LastName { get; set; }
    public string OldUserName { get; set; } = null!;
    public string? UserName { get; set; }
    public DateTime JoinDate { get; set; }
    [DataType(DataType.Password)] public string? OldPassword { get; set; }
    [DataType(DataType.Password)] public string? NewPassword { get; set; }
    [DataType(DataType.Password)] public string? NewPasswordConfirm { get; set; }
}