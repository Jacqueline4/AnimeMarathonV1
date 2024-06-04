using AnimeMarathon.Web.Pages;
using AnimeMarathon.Web.Pages.Shared;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient<LoginModel>();
builder.Services.AddHttpClient<EditUserModel>();
builder.Services.AddHttpClient<UserMenuModel>(); // Registrar HttpClient para UserMenuModel
builder.Services.AddHttpClient<AnimeDetailModel>();
builder.Services.AddScoped<SessionModel>();
//// Register IHttpClientFactory -- se añade para que la pagina index redireccione a Login
//builder.Services.AddHttpClient();


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddLogging();
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Login";
//        options.LogoutPath = "/Logout";
//    });

//builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();
