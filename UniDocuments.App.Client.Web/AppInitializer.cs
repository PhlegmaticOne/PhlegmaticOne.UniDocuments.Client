using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using PhlegmaticOne.ApiRequesting.Extensions;
using PhlegmaticOne.LocalStorage.Extensions;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Account;

namespace UniDocuments.App.Client.Web;

public static class AppInitializer
{
    public static WebApplication BuildApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews();
        
        builder.Services.AddValidatorsFromAssemblyContaining<Program>();

        builder.Services.AddAutoMapper(_ => { }, typeof(Program).Assembly);
        
        builder.Services.AddStorage();

        builder.Services.AddClientRequestsService("http://localhost:5109/api/", a =>
        {
            a.ConfigureRequest<RegisterProfileRequest>("Auth/Register");
            a.ConfigureRequest<LoginProfileRequest>("Auth/Login");
            a.ConfigureRequest<UpdateProfileRequest>("Profiles/Update");
        });

        builder.Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(x =>
            {
                x.LoginPath = new PathString("/Auth/Login");
            });
        
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

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}