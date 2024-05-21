using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account.Base;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Base;
using UniDocuments.App.Shared.Users.Enums;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Admin;

public class AdminMakeAdminViewModel : ErrorHaving
{
    [Required(ErrorMessage = "Имя пользователя не может быть пустым")]
    public string UserName { get; set; } = null!;

    public string Role { get; set; } = null!;
    public string AppRole { get; set; } = null!;
    public List<SelectListItem> Roles { get; set; } = new()
    {
        new SelectListItem("Студент", "Student"),
        new SelectListItem("Преподаватель", "Teacher")
    };
    
    public List<SelectListItem> AppRoles { get; set; } = new()
    {
        new SelectListItem("Пользователь", "Default"),
        new SelectListItem("Админ", "Admin")
    };

    public string RoleView => Roles.First(x => x.Value == Role.ToString()).Text;
    public string AppRoleView => AppRoles.First(x => x.Value == AppRole.ToString()).Text;
    public string? SuccessMessage { get; set; }
}