using AnimeMarathon.Web.Pages;
using AnimeMarathon.Web.Pages.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient<LoginModel>();
builder.Services.AddHttpClient<_UserMenuPartialModel>();
builder.Services.AddHttpClient<UserMenuModel>(); // Registrar HttpClient para UserMenuModel
builder.Services.AddHttpClient<AnimeDetailModel>();
//// Register IHttpClientFactory -- se añade para que la pagina index redireccione a Login
//builder.Services.AddHttpClient();


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddLogging();

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

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();
