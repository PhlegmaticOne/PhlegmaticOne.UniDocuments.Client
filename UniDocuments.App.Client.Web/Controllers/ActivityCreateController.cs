using System.Linq.Expressions;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhlegmaticOne.ApiRequesting.Services;
using PhlegmaticOne.LocalStorage;
using PhlegmaticOne.OperationResults;
using UniDocuments.App.Client.Web.Controllers.Base;
using UniDocuments.App.Client.Web.Infrastructure.Extensions;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Activities;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Activities;
using UniDocuments.App.Shared.Activities.Create;

namespace UniDocuments.App.Client.Web.Controllers;

[Authorize]
public class ActivityCreateController : ClientRequestsController
{
    private readonly IValidator<ActivityCreateViewModel> _validator;

    public ActivityCreateController(
        IClientRequestsService clientRequestsService, 
        IStorageService storageService, 
        IMapper mapper, IValidator<ActivityCreateViewModel> validator) : 
        base(clientRequestsService, storageService, mapper)
    {
        _validator = validator;
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View(new ActivityCreateViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public Task<IActionResult> Create(
        [Bind("TeacherId,Name,Description,StartDate,EndDate,Students")] ActivityCreateViewModel viewModel)
    {
        Validate(viewModel);
        
        if (ModelState.IsValid == false)
        {
            IActionResult view = View(viewModel);
            return Task.FromResult(view);
        }
        
        return Task.FromResult<IActionResult>(View(viewModel));
        // var activityCreateObject = Mapper.Map<ActivityCreateObject>(viewModel);
        //
        // return AuthorizedPost(new RequestCreateActivity(activityCreateObject), result =>
        // {
        //     var view = HomeView();
        //     return Task.FromResult(view);
        // }, onOperationFailed: result =>
        // {
        //     ProcessViewModelOnError(viewModel, result);
        //     return View(viewModel);
        // });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddStudent([Bind("Students")] ActivityCreateViewModel activity)
    {
        activity.Students.Add(new ActivityCreateStudentViewModel());
        return PartialView("ActivityCreateStudentsList", activity);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RemoveStudent([Bind("Students")] ActivityCreateViewModel activity)
    {
        activity.Students.RemoveAt(activity.Students.Count - 1);
        return PartialView("ActivityCreateStudentsList", activity);
    }

    private void ProcessViewModelOnError(ActivityCreateViewModel viewModel, OperationResult result)
    {
        var errorData = result.GetResult<List<string>>();
        var studentNames = viewModel.Students.Select(x => x.UserName).ToList();
        var indexes = errorData.Select(x => studentNames.IndexOf(x));

        foreach (var index in indexes)
        {
            Expression<Func<ActivityCreateViewModel, string>> expression = x => x.Students[index].UserName;
            var key = expression.GetExpressionText();
            ModelState.AddModelError(key, "Студент не найден!");
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