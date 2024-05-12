namespace UniDocuments.App.Client.Web.Infrastructure.Roles;

public static class RequireRolesExtensions
{
    public static IApplicationBuilder UseRequireAppRoles(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequireAppRolesMiddleware>();
    }
    
    public static IApplicationBuilder UseRequireStudyRoles(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequireStudyRolesMiddleware>();
    }
}