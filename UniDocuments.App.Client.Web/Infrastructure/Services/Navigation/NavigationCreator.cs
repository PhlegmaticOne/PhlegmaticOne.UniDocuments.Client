using System.Security.Claims;
using UniDocuments.App.Client.Web.Infrastructure.Extensions;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Home;
using UniDocuments.App.Shared.Users.Enums;

namespace UniDocuments.App.Client.Web.Infrastructure.Services.Navigation;

public class NavigationCreator : INavigationCreator
{
    public List<NavigationCardViewModel> Create(ClaimsPrincipal user)
    {
        var result = new List<NavigationCardViewModel>
        {
            new("Профиль", "Перейти", "Profile", "Details", "fa-solid fa-user-shield text-success fa-3x"),
            new("Профиль", "Обновить", "Profile", "Update", "fa-solid fa-user-pen text-warning fa-3x"),
        };

        switch (user.StudyRole())
        {
            case StudyRole.Student:
                FillForStudents(result);
                break;
            case StudyRole.Teacher:
                FillForTeachers(result);
                break;
        }

        if (user.AppRole() == AppRole.Admin)
        {
            FillForAdmins(result);
        }
        
        return result;
    }

    private static void FillForStudents(ICollection<NavigationCardViewModel> cardViewModels)
    {
        cardViewModels.Add(new("Мои активности", "Перейти", "Activities", "My", "fa-solid fa-people-group text-success fa-3x"));
    }

    private static void FillForTeachers(ICollection<NavigationCardViewModel> cardViewModels)
    {
        cardViewModels.Add(new("Созданные активности", "Перейти", "Activities", "Created", "fa-solid fa-people-group text-success fa-3x"));
        cardViewModels.Add(new("Активность", "Созданть", "ActivityCreate", "Create", "fa-solid fa-user-plus text-success fa-3x"));
        cardViewModels.Add(new("Документы", "Проверить", "Documents", "DetailedCheckDocument", "fa-solid fa-file-import text-warning fa-3x"));
    }

    private static void FillForAdmins(ICollection<NavigationCardViewModel> cardViewModels)
    {
        cardViewModels.Add(new("Админка", "Перейти", "AdminTools", "Index", "fa-solid fa-user-secret text-danger fa-3x"));
    }
}