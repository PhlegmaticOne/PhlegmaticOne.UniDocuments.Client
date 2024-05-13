using System.ComponentModel.DataAnnotations;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account.Base;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Admin;

public class AdminMakeAdminViewModel : RoleListHaving
{
    [Required]
    public string UserName { get; set; } = null!;

    public string? SuccessMessage { get; set; }
}