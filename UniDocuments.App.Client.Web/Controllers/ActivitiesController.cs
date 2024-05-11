using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhlegmaticOne.ApiRequesting.Services;
using PhlegmaticOne.LocalStorage;
using UniDocuments.App.Client.Web.Controllers.Base;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Activities;
using UniDocuments.App.Client.Web.Infrastructure.Roles;
using UniDocuments.App.Shared.Shared;
using UniDocuments.App.Shared.Users.Enums;

namespace UniDocuments.App.Client.Web.Controllers;

[Authorize]
public class ActivitiesController : ClientRequestsController
{
    public ActivitiesController(
        IClientRequestsService clientRequestsService,
        IStorageService storageService, 
        IMapper mapper) : base(clientRequestsService, storageService, mapper) { }

    
    [RequireStudyRoles(StudyRole.Teacher)]
    public Task<IActionResult> CreatedActivities(int? pageIndex, int? pageSize)
    {
        var pageData = new PagedListData
        {
            PageIndex = pageIndex is null ? 0 : pageIndex.Value - 1,
            PageSize = pageSize ?? 15
        };
        
        return AuthorizedGet(new RequestGetActivitiesTeacher(pageData), result =>
        {
            ViewData["PageSize"] = pageData.PageSize;
            IActionResult view = View(result.Activities);
            return Task.FromResult(view);
        });
    }

    public IActionResult Watch()
    {
        return Redirect("/Home/Index");
    }
}