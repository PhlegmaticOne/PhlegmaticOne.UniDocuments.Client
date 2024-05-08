using UniDocuments.App.Shared.Users.Enums;

namespace UniDocuments.App.Client.Web.Infrastructure.ViewModels.Account;

public class ProfileViewModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public StudyRole Role { get; set; }
}