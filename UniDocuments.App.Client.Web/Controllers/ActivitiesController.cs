using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhlegmaticOne.ApiRequesting.Services;
using PhlegmaticOne.LocalStorage;
using UniDocuments.App.Client.Web.Controllers.Base;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Activities;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Documents;
using UniDocuments.App.Client.Web.Infrastructure.Roles;
using UniDocuments.App.Shared.Activities.Detailed;
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

    
    [HttpGet]
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

    [HttpGet]
    public IActionResult Detailed()
    {
        var activity = new ActivityDetailedObject
        {
            Name = "ИТИ-41",
            Description = "Сдать срочно",
            EndDate = DateTime.UtcNow.AddDays(4),
            StartDate = DateTime.UtcNow.AddDays(1),
            CreatorFirstName = "Курочка",
            CreatorLastName = "Курочка",
            CreationDate = DateTime.MinValue,
            Students = new List<ActivityDetailedStudentObject>()
            {
                new()
                {
                    FirstName = "Александр",
                    LastName = "Невский",
                    Document = new ActivityDetailedDocumentObject
                    {
                        Id = Guid.NewGuid(),
                        Name = "test.docx",
                        DateLoaded = DateTime.UtcNow
                    }
                },
                new()
                {
                    FirstName = "Александр",
                    LastName = "Невский",
                    Document = null
                },
            }
        };

        return View(activity);
    }

    [HttpGet]
    [RequireStudyRoles(StudyRole.Student)]
    public Task<IActionResult> MyActivities(int? pageIndex, int? pageSize)
    {
        return null;
    }

    public Task<IActionResult> Download(Guid documentId)
    {
        return AuthorizedDownloadFile(new RequestDownloadDocument(documentId));
    }

    public Task<IActionResult> Check(Guid documentId)
    {
        return AuthorizedDownloadFile(new RequestCheckDocument(documentId));
    }

    public IActionResult DetailedCheck()
    {
        throw new NotImplementedException();
    }
}