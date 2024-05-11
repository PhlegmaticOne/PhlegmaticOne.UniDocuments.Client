using System.ComponentModel.DataAnnotations;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account.Base;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account;

public class LoginViewModel : RoleListHaving
{
    public string UserName { get; set; } = null!;
    public string? ReturnUrl { get; set; }
    [DataType(DataType.Password)] public string Password { get; set; } = null!;
}