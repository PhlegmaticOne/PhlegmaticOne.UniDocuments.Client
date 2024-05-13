﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhlegmaticOne.ApiRequesting.Services;
using UniDocuments.App.Client.Web.Controllers.Base;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Documents;
using UniDocuments.App.Client.Web.Infrastructure.Roles;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Document;
using UniDocuments.App.Shared.Documents;
using UniDocuments.App.Shared.Users.Enums;

namespace UniDocuments.App.Client.Web.Controllers;

[Authorize]
public class DocumentsController : ClientRequestsController
{
    public DocumentsController(
        IClientRequestsService clientRequestsService, IMapper mapper) : 
        base(clientRequestsService, mapper) { }
    
    [HttpGet]
    [RequireStudyRoles(StudyRole.Teacher)]
    public Task<IActionResult> DefaultCheck(Guid documentId)
    {
        return DownloadFile(new RequestDefaultCheckDocument(documentId));
    }

    [HttpGet]
    [RequireStudyRoles(StudyRole.Teacher)]
    public IActionResult DetailedCheck(
        Guid documentId, string documentName, DateTime dateLoaded, string firstName, string lastName)
    {
        var viewModel = new DocumentCheckViewModel
        {
            Name = documentName,
            DocumentId = documentId,
            DateLoaded = dateLoaded,
            FirstName = firstName,
            LastName = lastName,
        };
        
        return View(viewModel);
    }

    [HttpPost]
    [RequireStudyRoles(StudyRole.Teacher)]
    public Task<IActionResult> DetailedCheck(DocumentCheckViewModel viewModel)
    {
        var map = Mapper.Map<DocumentDetailedCheckObject>(viewModel);
        return DownloadFile(new RequestDetailedCheckDocument(map));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequireStudyRoles(StudyRole.Student)]
    public Task<IActionResult> Upload([FromForm] DocumentUploadObject viewModel)
    {
        return PostForm(new RequestUploadDocument(viewModel), _ => RedirectToAction("My", "Activities"));
    }

    public Task<IActionResult> Download(Guid documentId)
    {
        return DownloadFile(new RequestDownloadDocument(documentId));
    }
}