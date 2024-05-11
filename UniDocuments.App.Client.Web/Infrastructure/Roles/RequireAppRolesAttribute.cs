using UniDocuments.App.Shared.Users.Enums;

namespace UniDocuments.App.Client.Web.Infrastructure.Roles;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequireAppRolesAttribute : Attribute
{
    public AppRole[] AppRoles { get; }

    public RequireAppRolesAttribute(params AppRole[] appRoles)
    {
        AppRoles = appRoles;
    }
}