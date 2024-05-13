using UniDocuments.App.Shared.Users.Enums;

namespace UniDocuments.App.Shared.Admin;

public class AdminCreateObject
{
    public string UserName { get; set; } = null!;
    public StudyRole StudyRole { get; set; }
}