using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhlegmaticOne.ApiRequesting.Services;
using UniDocuments.App.Client.Web.Controllers.Base;
using UniDocuments.App.Client.Web.Infrastructure.Extensions;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Activities;
using UniDocuments.App.Client.Web.Infrastructure.Roles;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Activities;
using UniDocuments.App.Shared.Activities.Create;
using UniDocuments.App.Shared.Users.Enums;

namespace UniDocuments.App.Client.Web.Controllers;

[Authorize]
public class ActivityCreateController : ClientRequestsController
{
    private const string StudentNotFoundErrorMessage = "Студент не найден!";
    private const string StudentErrorExpression = "Students[{0}].UserName";
    
    private readonly IValidator<ActivityCreateViewModel> _validator;

    public ActivityCreateController(
        IClientRequestsService clientRequestsService, IMapper mapper,
        IValidator<ActivityCreateViewModel> validator) : 
        base(clientRequestsService, mapper)
    {
        _validator = validator;
    }
    
    [HttpGet]
    [RequireStudyRoles(StudyRole.Teacher)]
    public IActionResult Create()
    {
        return View(new ActivityCreateViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequireStudyRoles(StudyRole.Teacher)]
    public Task<IActionResult> Create(
        [Bind("TeacherId,Name,Description,StartDate,EndDate,Students")] ActivityCreateViewModel viewModel)
    {
        Validate(viewModel);
        
        if (ModelState.IsValid == false)
        {
            IActionResult view = View(viewModel);
            return Task.FromResult(view);
        }
        
        var activityCreateObject = Mapper.Map<ActivityCreateObject>(viewModel);
        
        return Post(new RequestCreateActivity(activityCreateObject), 
            result => View("~/Views/Activities/Detailed.cshtml", result), 
            result => 
            { 
                var errorStudents = JsonConvert.DeserializeObject<List<string>>(result.ErrorData!)!; 
                ProcessViewModelOnError(viewModel, errorStudents); 
                return View(viewModel); 
            });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequireStudyRoles(StudyRole.Teacher)]
    public IActionResult AddStudent([Bind("Students")] ActivityCreateViewModel activity)
    {
        activity.Students.Add(new ActivityCreateStudentViewModel());
        return PartialView("~/Views/ActivityCreate/PartialViews/ActivityCreateStudentsList.cshtml", activity);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequireStudyRoles(StudyRole.Teacher)]
    public IActionResult RemoveStudent([Bind("Students")] ActivityCreateViewModel activity)
    {
        activity.Students.RemoveLast();
        return PartialView("~/Views/ActivityCreate/PartialViews/ActivityCreateStudentsList.cshtml", activity);
    }

    private void ProcessViewModelOnError(ActivityCreateViewModel viewModel, List<string> errorData)
    {
        var studentNames = viewModel.Students.Select(x => x.UserName).ToList();
        var indexes = errorData.Select(x => studentNames.IndexOf(x));

        foreach (var index in indexes)
        {
            var key = string.Format(StudentErrorExpression, index);
            ModelState.AddModelError(key, StudentNotFoundErrorMessage);
        }
    }
    
    private void Validate(ActivityCreateViewModel viewModel)
    {
        var validationResult = _validator.Validate(viewModel);

        if (validationResult.IsValid == false)
        {
            validationResult.AddToModelState(ModelState);
        }
    }
}