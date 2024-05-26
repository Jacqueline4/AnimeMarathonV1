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


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AnimeMarathonContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AnimeMarathonConnection")));


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

//builder.Services.AddScoped(typeof(IBaseServices<>), typeof(BaseServices<>));
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Habilitar CORS
app.Use(async (context, next) =>
{
    // Agregar el encabezado Access-Control-Allow-Origin a todas las respuestas
    context.Response.Headers.Append("Access-Control-Allow-Origin", "https://localhost:7146");

    // Continuar con el procesamiento de la solicitud
    await next();
});

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();
