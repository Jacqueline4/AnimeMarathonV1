using AnimeMarahon.Core.Repositories;
using AnimeMarahon.Core.Repositories.Base;
using AnimeMarathon.Application.Interfaces;
using AnimeMarathon.Application.Services;
using AnimeMarathon.Data.Data;
using AnimeMarathon.Data.Repository;
using AnimeMarathon.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AnimeMarathonContext>(c =>
c.UseInMemoryDatabase("AnimeMarathonConnection"));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();

builder.Services.AddScoped<IAnimeService,AnimesService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
