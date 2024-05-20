using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniDocuments.ApiRequesting.Services;
using UniDocuments.App.Client.Web.Controllers.Base;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Admin;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Documents;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Neural;
using UniDocuments.App.Client.Web.Infrastructure.Roles;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Admin;
using UniDocuments.App.Client.Web.Infrastructure.ViewModels.Neural;
using UniDocuments.App.Shared.Admin;
using UniDocuments.App.Shared.Neural;
using UniDocuments.App.Shared.Users.Enums;

namespace UniDocuments.App.Client.Web.Controllers;

[Authorize]
public class AdminToolsController : ClientRequestsController
{
    private const string AdminSuccessFormat = "Пользователь {0} теперь админ!";
    private const string UserNotFoundMessage = "Пользователь не найден";
    
    private readonly IValidator<NeuralTrainDoc2VecViewModel> _doc2VecValidator;
    private readonly IValidator<NeuralTrainKerasViewModel> _kerasValidator;
    
    public AdminToolsController(
        IClientRequestsService clientRequestsService, IMapper mapper,
        IValidator<NeuralTrainDoc2VecViewModel> doc2VecValidator,
        IValidator<NeuralTrainKerasViewModel> kerasValidator) :
        base(clientRequestsService, mapper)
    {
        _doc2VecValidator = doc2VecValidator;
        _kerasValidator = kerasValidator;
    }
    
    [HttpGet]
    [RequireAppRoles(AppRole.Admin)]
    public IActionResult Index()
    {
        return View("MakeAdmin", new AdminMakeAdminViewModel());
    }

    [HttpGet]
    [RequireAppRoles(AppRole.Admin)]
    public IActionResult MakeAdmin()
    {
        return View(new AdminMakeAdminViewModel());
    }
    
    [HttpGet]
    [RequireAppRoles(AppRole.Admin)]
    public Task<IActionResult> TrainKerasModel()
    {
        return Get(new RequestGetGlobalData(), data => View(new NeuralTrainKerasViewModel { GlobalData = data }));
    }
    
    [HttpGet]
    [RequireAppRoles(AppRole.Admin)]
    public Task<IActionResult> TrainDoc2VecModel()
    {
        return Get(new RequestGetGlobalData(), data => View(new NeuralTrainDoc2VecViewModel { GlobalData = data }));
    }

    [HttpGet]
    [RequireAppRoles(AppRole.Admin)]
    public IActionResult RebuildDocuments()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequireAppRoles(AppRole.Admin)]
    public Task<IActionResult> MakeAdmin(AdminMakeAdminViewModel viewModel)
    {
        if (ModelState.IsValid == false)
        {
            return Task.FromResult<IActionResult>(View(viewModel));
        }
        
        var data = Mapper.Map<AdminCreateObject>(viewModel);
        
        return Post(new RequestMakeAdmin(data),
            _ =>
            {
                viewModel.SuccessMessage = string.Format(AdminSuccessFormat, viewModel.UserName);
                return View(viewModel);
            },
            _ =>
            {
                ModelState.AddModelError(nameof(AdminMakeAdminViewModel.UserName), UserNotFoundMessage);
                return View(viewModel);
            });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequireAppRoles(AppRole.Admin)]
    public Task<IActionResult> TrainKerasModel(NeuralTrainKerasViewModel viewModel)
    {
        ValidateKerasViewModel(viewModel);
        
        if (!ModelState.IsValid)
        {
            return Task.FromResult<IActionResult>(View(viewModel));
        }
        
        var data = Mapper.Map<NeuralTrainOptionsKeras>(viewModel);
        
        return Post(new RequestTrainKeras(data), 
            result => View("TrainKerasResult", result),
            result => View("TrainKerasResult", result.GetErrorDataAs<NeuralTrainResultKeras>()));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequireAppRoles(AppRole.Admin)]
    public Task<IActionResult> TrainDoc2VecModel(NeuralTrainDoc2VecViewModel viewModel)
    {
        ValidateDoc2VecViewModel(viewModel);
        
        if (!ModelState.IsValid)
        {
            return Task.FromResult<IActionResult>(View(viewModel));
        }
        
        var data = Mapper.Map<NeuralTrainOptionsDoc2Vec>(viewModel);

        return Post(new RequestTrainDoc2Vec(data),
            result => View("TrainDoc2VecResult", result),
            result => View("TrainDoc2VecResult", result.GetErrorDataAs<NeuralTrainResultDoc2Vec>()));
    }
    
    [HttpGet]
    [RequireAppRoles(AppRole.Admin)]
    public Task<IActionResult> RebuildDocumentsExecute()
    {
        return Post(new RequestRebuildDocuments(), result => View("RebuildDocumentsResult", result));
    }
    
    private void ValidateDoc2VecViewModel(NeuralTrainDoc2VecViewModel viewModel)
    {
        var validationResult = _doc2VecValidator.Validate(viewModel);

        if (validationResult.IsValid == false)
        {
            validationResult.AddToModelState(ModelState);
        }
    }
    
    private void ValidateKerasViewModel(NeuralTrainKerasViewModel viewModel)
    {
        var validationResult = _kerasValidator.Validate(viewModel);

        if (validationResult.IsValid == false)
        {
            validationResult.AddToModelState(ModelState);
        }
    }
}