using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using UniDocuments.App.Client.Web.Infrastructure.Extensions;

namespace UniDocuments.App.Client.Web.Infrastructure.Roles;

public class RequireStudyRolesMiddleware
{
    private readonly RequestDelegate _next;

    public RequireStudyRolesMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var studyRole = context.User.StudyRole();
        var endpoint = context.GetEndpoint();

        if (endpoint is not null)
        {
            var endpointActionData = endpoint.Metadata.GetRequiredMetadata<ControllerActionDescriptor>();
            var studyRolesAttribute = endpointActionData.MethodInfo.GetCustomAttribute<RequireStudyRolesAttribute>();

            if (studyRolesAttribute is not null && studyRolesAttribute.StudyRoles.Contains(studyRole) == false)
            {
                context.Response.Redirect("/Home/RestrictedPage");
                return;
            }
        }
        
        await _next(context);
    }
}