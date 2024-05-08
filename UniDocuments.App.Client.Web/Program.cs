using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using PhlegmaticOne.ApiRequesting.Extensions;
using PhlegmaticOne.LocalStorage.Extensions;
using UniDocuments.App.Client.Web.Infrastructure.Requests.Account;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x => x.LoginPath = new PathString("/Auth/Login"));

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddAutoMapper(_ => { }, typeof(Program).Assembly);
builder.Services.AddClientRequestsService("http://localhost:5109/api/", a =>
{
    a.ConfigureRequest<RegisterProfileRequest>("Auth/Register");
    a.ConfigureRequest<LoginProfileRequest>("Auth/Login");
    a.ConfigureRequest<UpdateProfileRequest>("Profiles/Update");
});

builder.Services.AddStorage();

var app = builder.Build();

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