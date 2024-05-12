using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using PhlegmaticOne.ApiRequesting.Extensions;
using PhlegmaticOne.LocalStorage.Extensions;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Account;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Activities;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Documents;
using UniDocuments.App.Client.Web.Infrastructure.Roles;
using UniDocuments.App.Client.Web.Infrastructure.TagHelpers.PagedList.Helpers;

namespace UniDocuments.App.Client.Web;

public static class AppInitializer
{
    public static WebApplication BuildApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
        
        builder.Services.AddValidatorsFromAssemblyContaining<Program>();

        builder.Services.AddAutoMapper(_ => { }, typeof(Program).Assembly);
        
        builder.Services.AddStorage();
        
        builder.Services.AddClientRequestsService("http://localhost:5109/api/", a =>
        {
            a.ConfigureRequest<RequestRegister>("Auth/Register");
            a.ConfigureRequest<RequestLogin>("Auth/Login");
            a.ConfigureRequest<RequestUpdateProfile>("Profiles/Update");
            
            a.ConfigureRequest<RequestGetActivitiesTeacher>("Activities/GetForTeacher");
            a.ConfigureRequest<RequestGetActivitiesStudent>("Activities/GetForStudent");
            a.ConfigureRequest<RequestGetDetailedActivity>("Activities/GetDetailed");
            a.ConfigureRequest<RequestCreateActivity>("Activities/Create");
            
            a.ConfigureRequest<RequestDownloadDocument>("Documents/GetFileById");
            a.ConfigureRequest<RequestCheckDocument>("Documents/GetFileById");
            a.ConfigureRequest<RequestUploadDocument>("Documents/Upload");
        });

        builder.Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(x =>
            {
                x.LoginPath = new PathString("/Auth/Login");
            });
        
        builder.Services.AddScoped<IPagedListPagesGenerator, PagedListPagesGenerator>();
        
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
        app.UseMiddleware<RequireAppRolesMiddleware>();
        app.UseMiddleware<RequireStudyRolesMiddleware>();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}