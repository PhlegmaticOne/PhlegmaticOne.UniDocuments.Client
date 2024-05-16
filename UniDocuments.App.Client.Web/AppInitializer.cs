using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using PhlegmaticOne.ApiRequesting.Extensions;
using UniDocuments.App.Client.Web.Infrastructure.Requests;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Account;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Activities;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Admin;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Documents;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Neural;
using UniDocuments.App.Client.Web.Infrastructure.Roles;
using UniDocuments.App.Client.Web.Infrastructure.Services.Navigation;
using UniDocuments.App.Client.Web.Infrastructure.TagHelpers.PagedList.Helpers;

namespace UniDocuments.App.Client.Web;

public static class AppInitializer
{
    public static WebApplication BuildApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
        
        builder.Services.AddValidatorsFromAssemblyContaining<Program>();

        builder.Services.AddAutoMapper(_ => { }, typeof(Program).Assembly);
        
        builder.Services.AddClientRequestsService("http://localhost:5109/api/", a =>
        {
            a.ConfigureRequest<RequestRegister>("Auth/Register");
            a.ConfigureRequest<RequestLogin>("Auth/Login");
            a.ConfigureRequest<RequestUpdateProfile>("Profiles/Update");
            a.ConfigureRequest<RequestMakeAdmin>("Profiles/MakeAdmin");
            
            a.ConfigureRequest<RequestGetActivitiesTeacher>("Activities/GetForTeacher");
            a.ConfigureRequest<RequestGetActivitiesStudent>("Activities/GetForStudent");
            a.ConfigureRequest<RequestGetDetailedActivity>("Activities/GetDetailed");
            a.ConfigureRequest<RequestCreateActivity>("Activities/Create");
            
            a.ConfigureRequest<RequestDownloadDocument>("Documents/Download");
            a.ConfigureRequest<RequestUploadDocument>("Documents/Upload");
            a.ConfigureRequest<RequestGetGlobalData>("Documents/GetGlobalData");
            
            a.ConfigureRequest<RequestTrainDoc2Vec>("NeuralModel/TrainDoc2Vec");
            a.ConfigureRequest<RequestTrainKeras>("NeuralModel/TrainKeras");
            
            a.ConfigureRequest<RequestDetailedCheckDocument>("Reports/BuildForExistingDocument");
            a.ConfigureRequest<RequestDetailedCheckDocumentNew>("Reports/BuildForDocument");
            a.ConfigureRequest<RequestDefaultCheckDocument>("Reports/BuildExistingDocumentDefault");
            
            a.ConfigureRequest<RequestGetStatistics>("Statistics/GetStatistics");
        });

        builder.Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(x =>
            {
                x.LoginPath = new PathString("/Auth/Login");
            });
        
        builder.Services.AddScoped<IPagedListPagesGenerator, PagedListPagesGenerator>();
        builder.Services.AddSingleton<INavigationCreator, NavigationCreator>();
        
        return builder.Build();
    }

    public static void RunApplication(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseRequireAppRoles();
        app.UseRequireStudyRoles();
        
        app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
        
        app.Run();
    }
}