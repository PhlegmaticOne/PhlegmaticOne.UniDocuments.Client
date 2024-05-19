using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniDocuments.ApiRequesting.Services;
using UniDocuments.App.Client.Web.Controllers.Base;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Activities;
using UniDocuments.App.Client.Web.Infrastructure.Roles;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels;
using UniDocuments.App.Shared.Shared;
using UniDocuments.App.Shared.Users.Enums;
using UniDocuments.Results;

namespace UniDocuments.App.Client.Web.Controllers;

[Authorize]
public class ActivitiesController : ClientRequestsController
{
    private const string ErrorMessageActivityNotFound = "Активность не найдена!";
    
    public ActivitiesController(
        IClientRequestsService clientRequestsService, IMapper mapper) : 
        base(clientRequestsService, mapper) { }
    
    [HttpGet]
    [RequireStudyRoles(StudyRole.Teacher)]
    public Task<IActionResult> Created(int? pageIndex, int? pageSize)
    {
        var pageData = PagedListData.FromQueryData(pageIndex, pageSize);
        
        return Get(new RequestGetActivitiesTeacher(pageData), result =>
        {
            ViewData["PageSize"] = pageData.PageSize;
            return View(result.Activities);
        });
    }

    [HttpGet]
    [RequireStudyRoles(StudyRole.Student)]
    public Task<IActionResult> My(int? pageIndex, int? pageSize)
    {
        var pageData = PagedListData.FromQueryData(pageIndex, pageSize);
        
        return Get(new RequestGetActivitiesStudent(pageData), result =>
        {
            ViewData["PageSize"] = pageData.PageSize;
            return View(result.Activities);
        });
    }

    [HttpGet]
    [RequireStudyRoles(StudyRole.Teacher)]
    public Task<IActionResult> Detailed(Guid activityId)
    {
        return Get(new RequestGetDetailedActivity(activityId), View, RedirectToErrorPageNotFound);
    }

    private IActionResult RedirectToErrorPageNotFound(OperationResult _)
    {
        return RedirectToAction("Error", "Home", new ErrorViewModel(ErrorMessageActivityNotFound));
    }
}