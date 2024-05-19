using System.ComponentModel.DataAnnotations;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account.Base;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account;

public class LoginViewModel : RoleListHaving
{
    [Required(ErrorMessage = "Ник не может быть пустым")]
    public string UserName { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Пароль не может быть пустым")] 
    public string Password { get; set; } = null!;

    public string? ReturnUrl { get; set; }
}