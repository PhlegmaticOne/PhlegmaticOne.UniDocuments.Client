using Microsoft.AspNetCore.Mvc.Rendering;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Base;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account.Base;

public class RoleListHaving : ErrorHaving
{
    public List<SelectListItem> Roles { get; set; } = new()
    {
        new SelectListItem("Студент", "Student"),
        new SelectListItem("Преподаватель", "Teacher")
    };

    public string Role { get; set; } = null!;
}