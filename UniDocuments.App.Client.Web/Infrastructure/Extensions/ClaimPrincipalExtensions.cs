using System.Security.Claims;
using UniDocuments.App.Shared.Users.Enums;

namespace UniDocuments.App.Client.Web.Infrastructure.Extensions;

public static class ProfileClaimsConstants
{
    internal const string IdClaimName = "Id";
    internal const string FirstNameClaimName = "FirstName";
    internal const string LastNameClaimName = "LastName";
    internal const string AppRoleClaimName = "AppRole";
    internal const string StudyRoleClaimName = "StudyRole";
    internal const string JoinDateClaimName = "JoinDate";
}

public static class ClaimPrincipalExtensions
{
    public static Guid Id(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.Claims
            .FirstOrDefault(x => x.Type == ProfileClaimsConstants.IdClaimName);

        return claim is null ? Guid.Empty : Guid.Parse(claim.Value);
    }

    public static string? IdString(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.Claims
            .FirstOrDefault(x => x.Type == ProfileClaimsConstants.IdClaimName);

        return claim?.Value;
    }
    
    public static DateTime JoinDate(this ClaimsPrincipal claimsPrincipal)
    {
        var claimValue = claimsPrincipal.Claims
            .FirstOrDefault(x => x.Type == ProfileClaimsConstants.JoinDateClaimName);

        if (claimValue is null)
        {
            return DateTime.MinValue;
        }
        
        return DateTime.Parse(claimValue.Value);
    }


    public static StudyRole StudyRole(this ClaimsPrincipal claimsPrincipal)
    {
        var claimValue = claimsPrincipal.Claims
            .FirstOrDefault(x => x.Type == ProfileClaimsConstants.StudyRoleClaimName);

        if (claimValue is null)
        {
            return Shared.Users.Enums.StudyRole.Student;
        }
        
        return (StudyRole)int.Parse(claimValue.Value);
    }

    public static AppRole AppRole(this ClaimsPrincipal claimsPrincipal)
    {
        var claimValue = claimsPrincipal.Claims
            .FirstOrDefault(x => x.Type == ProfileClaimsConstants.AppRoleClaimName);

        if (claimValue is null)
        {
            return Shared.Users.Enums.AppRole.Default;
        }
        
        return (AppRole)int.Parse(claimValue.Value);
    }

    public static string Username(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.Claims
            .FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType);
        return claim is null ? string.Empty : claim.Value;
    }
    
    public static string Firstname(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.Claims
            .FirstOrDefault(x => x.Type == ProfileClaimsConstants.FirstNameClaimName);
        return claim is null ? string.Empty : claim.Value;
    }

    public static string Lastname(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.Claims
            .FirstOrDefault(x => x.Type == ProfileClaimsConstants.LastNameClaimName);
        return claim is null ? string.Empty : claim.Value;
    }
}