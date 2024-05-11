using Microsoft.AspNetCore.Mvc.Rendering;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Base;
using UniDocuments.App.Shared.Users.Enums;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account.Base;

public class RoleViewsHaving : ErrorHaving
{
    public StudyRole Role { get; set; }
    public AppRole AppRole { get; set; }
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
}