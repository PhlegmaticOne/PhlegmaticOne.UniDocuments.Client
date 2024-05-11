using System.ComponentModel.DataAnnotations;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account.Base;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account;

public class RegisterViewModel : RoleListHaving
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    [DataType(DataType.Password)] public string Password { get; set; } = null!;
    [DataType(DataType.Password)] public string ConfirmPassword { get; set; } = null!;
}