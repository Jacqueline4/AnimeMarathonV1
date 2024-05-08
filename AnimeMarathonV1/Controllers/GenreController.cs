using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.Interfaces;
using AnimeMarathon.Application.Services;
using AnimeMarathonV1.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeMarathonV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IGenreService genreService;
        public GenreController(IMapper mapper, IGenreService genreService)
        {
            this.mapper = mapper;
            this.genreService= genreService;    
        }
        [HttpGet]
        public async Task<IEnumerable<GenreDTO>> Get()
        {
            var list = await genreService.GetGenreList();   
            var mapped = mapper.Map<IEnumerable<GenreDTO>>(list);
            return mapped;
        }

        [HttpGet("GetGenreByAnimeName/{animeName}")]
        public async Task<IEnumerable<GenreDTO>> GetGenre(string animeName)
        {
            if (string.IsNullOrWhiteSpace(animeName))
            {
                var list = await genreService.GetGenreList();   
                var mapped = mapper.Map<IEnumerable<GenreDTO>>(list);
                return mapped;
            }

            var listByName = await genreService.GetGenresByAnimeName(animeName);  
            var mappedByName = mapper.Map<IEnumerable<GenreDTO>>(listByName);
            return mappedByName;
        }

        [HttpPost]
        public async Task<GenreDTO> CreateGenre(GenreDTO genreViewModel)
        {
            var mapped = mapper.Map<Genre>(genreViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var entityDto = await genreService.Create(mapped);

            var mappedViewModel = mapper.Map<GenreDTO>(entityDto);
            return mappedViewModel;
        }

        [HttpPut]
        public async Task UpdateGenre(GenreDTO genreViewModel)
        {
            var mapped = mapper.Map<Genre>(genreViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            await genreService.Update(mapped);
        }

        [HttpDelete]
        public async Task DeleteGenre(int genreId)
        {
      
            await genreService.Delete(genreId);
        }
    }

}
