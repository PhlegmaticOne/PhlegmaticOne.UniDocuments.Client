using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using UniDocuments.App.Client.Web.Infrastructure.Extensions;
using UniDocuments.App.Shared.Users.Enums;

namespace UniDocuments.App.Client.Web.Infrastructure.Roles;

public class RequireAppRolesMiddleware
{
    private readonly RequestDelegate _next;

    public RequireAppRolesMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var appRole = context.User.AppRole();
        var endpoint = context.GetEndpoint();

        if (endpoint is not null && appRole != AppRole.Admin)
        {
            var endpointActionData = endpoint.Metadata.GetRequiredMetadata<ControllerActionDescriptor>();

            var appRolesAttribute = endpointActionData.MethodInfo.GetCustomAttribute<RequireAppRolesAttribute>();

            if (appRolesAttribute is not null && appRolesAttribute.AppRoles.Contains(appRole) == false)
            {
                context.Response.Redirect("/Home/UserUnauthorized");
            }
        }
        
        await _next(context);
    }
}