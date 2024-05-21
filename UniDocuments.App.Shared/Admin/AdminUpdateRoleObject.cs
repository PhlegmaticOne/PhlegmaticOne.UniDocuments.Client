using UniDocuments.App.Shared.Users.Enums;

namespace UniDocuments.App.Shared.Admin;

public class AdminUpdateRoleObject
{
    public string UserName { get; set; } = null!;
    public AppRole AppRole { get; set; }
    public StudyRole StudyRole { get; set; }
}