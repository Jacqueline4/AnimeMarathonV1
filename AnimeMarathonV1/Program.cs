using AnimeMarahon.Core.Repositories;
using AnimeMarahon.Core.Repositories.Base;
using AnimeMarathon.Application.Interfaces;
using AnimeMarathon.Application.Interfaces.Base;
using AnimeMarathon.Application.Services;
using AnimeMarathon.Application.Services.Base;
using AnimeMarathon.Application.Services.Mapper;
using AnimeMarathon.Data.Data;
using AnimeMarathon.Data.Repository;
using AnimeMarathon.Data.Repository.Base;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "AnimeMarathonAPI", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<AnimeMarathonContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AnimeMarathonConnection")));


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped(typeof(IBaseServices<,>), typeof(BaseServices<,>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAnimeService, AnimesService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddAutoMapper(System.Reflection.Assembly.GetAssembly(new AppMapperProfile().GetType()));


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost/*", "https://localhost:7146",
                "http://www.contoso.com").AllowAnyHeader().AllowAnyMethod();
        });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Access-Control-Allow-Origin", "https://localhost:7146");

    await next();
});

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();
