using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhlegmaticOne.ApiRequesting.Services;
using UniDocuments.App.Client.Web.Controllers.Base;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Activities;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Documents;
using UniDocuments.App.Client.Web.Infrastructure.Roles;
using UniDocuments.App.Shared.Documents;
using UniDocuments.App.Shared.Shared;
using UniDocuments.App.Shared.Users.Enums;

namespace UniDocuments.App.Client.Web.Controllers;

[Authorize]
public class ActivitiesController : ClientRequestsController
{
    public ActivitiesController(
        IClientRequestsService clientRequestsService, IMapper mapper) : 
        base(clientRequestsService, mapper) { }
    
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
            return View(result.Activities);
        });
    }

    [HttpGet]
    [RequireStudyRoles(StudyRole.Student)]
    public Task<IActionResult> MyActivities(int? pageIndex, int? pageSize)
    {
        var pageData = new PagedListData
        {
            PageIndex = pageIndex is null ? 0 : pageIndex.Value - 1,
            PageSize = pageSize ?? 15
        };
        
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
        return Get(new RequestGetDetailedActivity(activityId), View);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequireStudyRoles(StudyRole.Student)]
    public Task<IActionResult> Upload([FromForm] DocumentUploadObject viewModel)
    {
        return PostForm(new RequestUploadDocument(viewModel), _ => RedirectToAction("MyActivities"));
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