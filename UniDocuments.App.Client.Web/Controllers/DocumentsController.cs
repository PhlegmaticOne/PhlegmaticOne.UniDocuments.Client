using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniDocuments.ApiRequesting.Services;
using UniDocuments.App.Client.Web.Controllers.Base;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Documents;
using UniDocuments.App.Client.Web.Infrastructure.Roles;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Document;
using UniDocuments.App.Shared.Documents;
using UniDocuments.App.Shared.Documents.Search;
using UniDocuments.App.Shared.Users.Enums;
using UniDocuments.Results;

namespace UniDocuments.App.Client.Web.Controllers;

[Authorize]
public class DocumentsController : ClientRequestsController
{
    private const string ErrorMessageFileNotFound = "Файл не найден!";
    
    private readonly IValidator<DocumentsSearchViewModel> _searchValidator;

    public DocumentsController(
        IClientRequestsService clientRequestsService, IMapper mapper,
        IValidator<DocumentsSearchViewModel> searchValidator) : 
        base(clientRequestsService, mapper)
    {
        _searchValidator = searchValidator;
    }
    
    [HttpGet]
    [RequireStudyRoles(StudyRole.Teacher)]
    public Task<IActionResult> DefaultCheck(Guid documentId)
    {
        return DownloadFile(new RequestDefaultCheckDocument(documentId), ErrorResultFileNotFound);
    }

    [HttpGet]
    public IActionResult Search()
    {
        return View(new DocumentsSearchViewModel());
    }

    [HttpPost]
    public Task<IActionResult> Search([FromBody] DocumentsSearchViewModel? viewModel)
    {
        if (viewModel is null || !_searchValidator.Validate(viewModel).IsValid)
        {
            return Task.FromResult<IActionResult>(BadRequest(viewModel));
        }

        var searchObject = Mapper.Map<DocumentsSearchObject>(viewModel);

        return Post(new RequestSearchDocument(searchObject), result =>
        {
            return PartialView("~/Views/Documents/PartialViews/DocumentsSearchResult.cshtml", result);
        });
    } 

    [HttpGet]
    [RequireStudyRoles(StudyRole.Teacher)]
    public IActionResult DetailedCheck(
        Guid activityId, Guid documentId, string documentName, DateTime dateLoaded, string firstName, string lastName)
    {
        var viewModel = new DocumentCheckExistingViewModel
        {
            Name = documentName,
            DocumentId = documentId,
            DateLoaded = dateLoaded,
            FirstName = firstName,
            LastName = lastName,
            ActivityId = activityId
        };
        
        return View(viewModel);
    }

    [HttpGet]
    [RequireStudyRoles(StudyRole.Teacher)]
    public IActionResult DetailedCheckDocument()
    {
        return View(new DocumentCheckNewViewModel());
    }

    [HttpPost]
    [RequireStudyRoles(StudyRole.Teacher)]
    public Task<IActionResult> DetailedCheck(DocumentCheckExistingViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return Task.FromResult<IActionResult>(View(viewModel));
        }
        
        var map = Mapper.Map<DocumentDetailedCheckObject>(viewModel);
        return DownloadFile(new RequestDetailedCheckDocument(map), ErrorResultFileNotFound);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequireStudyRoles(StudyRole.Teacher)]
    public Task<IActionResult> DetailedCheckDocument(DocumentCheckNewViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return Task.FromResult<IActionResult>(View(viewModel));
        }
        
        var map = Mapper.Map<DocumentDetailedCheckDocumentObject>(viewModel);
        return DownloadFile(new RequestDetailedCheckDocumentNew(map), ErrorResultFileNotFound);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequireStudyRoles(StudyRole.Student)]
    public Task<IActionResult> Upload([FromForm] DocumentUploadObject viewModel)
    {
        return PostForm(new RequestUploadDocument(viewModel), _ => RedirectToAction("My", "Activities", new
        {
            pageIndex = viewModel.PageIndex,
            pageSize = viewModel.PageSize
        }));
    }

    public Task<IActionResult> Download(Guid documentId)
    {
        return DownloadFile(new RequestDownloadDocument(documentId), ErrorResultFileNotFound);
    }

    private IActionResult ErrorResultFileNotFound(OperationResult _)
    {
        return ErrorView(ErrorMessageFileNotFound);
    }
}