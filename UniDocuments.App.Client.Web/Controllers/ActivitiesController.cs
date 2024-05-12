using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhlegmaticOne.ApiRequesting.Services;
using PhlegmaticOne.LocalStorage;
using UniDocuments.App.Client.Web.Controllers.Base;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Activities;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Documents;
using UniDocuments.App.Client.Web.Infrastructure.Roles;
using UniDocuments.App.Shared.Shared;
using UniDocuments.App.Shared.Users.Enums;

namespace UniDocuments.App.Client.Web.Controllers;

[Authorize]
public class ActivitiesController : ClientRequestsController
{
    public ActivitiesController(
        IClientRequestsService clientRequestsService, IStorageService storageService, IMapper mapper) : 
        base(clientRequestsService, storageService, mapper) { }
    
    [HttpGet]
    [RequireStudyRoles(StudyRole.Teacher)]
    public Task<IActionResult> CreatedActivities(int? pageIndex, int? pageSize)
    {
        var pageData = new PagedListData
        {
            PageIndex = pageIndex is null ? 0 : pageIndex.Value - 1,
            PageSize = pageSize ?? 15
        };
        
        return Get(new RequestGetActivitiesTeacher(pageData), result =>
        {
            ViewData["PageSize"] = pageData.PageSize;
            IActionResult view = View(result.Activities);
            return Task.FromResult(view);
        });
    }

    [HttpGet]
    [RequireStudyRoles(StudyRole.Teacher)]
    public Task<IActionResult> Detailed(Guid activityId)
    {
        return Get(new RequestGetDetailedActivity(activityId), result =>
        {
            IActionResult view = View(result);
            return Task.FromResult(view);
        });
    }

    [HttpGet]
    [RequireStudyRoles(StudyRole.Student)]
    public Task<IActionResult> MyActivities(int? pageIndex, int? pageSize)
    {
        return null;
    }

    public Task<IActionResult> Download(Guid documentId)
    {
        return DownloadFile(new RequestDownloadDocument(documentId));
    }

    public Task<IActionResult> Check(Guid documentId)
    {
        return DownloadFile(new RequestCheckDocument(documentId));
    }

    public IActionResult DetailedCheck()
    {
        throw new NotImplementedException();
    }
}