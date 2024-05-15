using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhlegmaticOne.ApiRequesting.Services;
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
    
    public AdminToolsController(
        IClientRequestsService clientRequestsService, IMapper mapper) :
        base(clientRequestsService, mapper) { }

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
        return Get(new RequestGetGlobalData(), data =>
        {
            var viewModel = new NeuralTrainKerasViewModel
            {
                GlobalData = data
            };

            return View(viewModel);
        });
    }
    
    [HttpGet]
    [RequireAppRoles(AppRole.Admin)]
    public Task<IActionResult> TrainDoc2VecModel()
    {
        return Get(new RequestGetGlobalData(), data =>
        {
            var viewModel = new NeuralTrainDoc2VecViewModel()
            {
                GlobalData = data
            };

            return View(viewModel);
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequireAppRoles(AppRole.Admin)]
    public Task<IActionResult> MakeAdmin(AdminMakeAdminViewModel viewModel)
    {
        if (ModelState.IsValid == false)
        {
            IActionResult view = View(viewModel);
            return Task.FromResult(view);
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
        var data = Mapper.Map<NeuralTrainOptionsKeras>(viewModel);
        
        return Post(new RequestTrainKeras(data), result =>
        {
            viewModel.TrainResult = result;
            return View(viewModel);
        });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequireAppRoles(AppRole.Admin)]
    public Task<IActionResult> TrainDoc2VecModel(NeuralTrainDoc2VecViewModel viewModel)
    {
        var data = Mapper.Map<NeuralTrainOptionsDoc2Vec>(viewModel);
        
        return Post(new RequestTrainDoc2Vec(data), result =>
        {
            viewModel.TrainResult = result;
            return View(viewModel);
        });
    }
}