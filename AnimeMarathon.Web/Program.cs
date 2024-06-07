using AnimeMarathon.Web.Pages;
using AnimeMarathon.Web.Pages.Shared;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient<LoginModel>();
builder.Services.AddHttpClient<EditUserModel>();
builder.Services.AddHttpClient<UserMenuModel>();
builder.Services.AddHttpClient<AnimeDetailModel>();
builder.Services.AddHttpClient<AnimesByGenreModel>();
builder.Services.AddHttpClient<AnimesByCategoryModel>();
builder.Services.AddHttpClient<LogoutModel>();
builder.Services.AddHttpClient<RegisterModel>();
builder.Services.AddScoped<SessionModel>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddLogging();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();
