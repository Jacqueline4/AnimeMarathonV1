using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.Interfaces;
using AnimeMarathonV1.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeMarathonV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimeGenreController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAnimeGenreService animeGenreService;
        public AnimeGenreController(IMapper mapper, IAnimeGenreService animeGenreService)
        {
            this.mapper = mapper;
            this.animeGenreService = animeGenreService;
        }
        [HttpGet]
        public async Task<IEnumerable<AnimeGenreDTO>> Get()
        {
            var list = await animeGenreService.GetAnimeGenreList();
            var mapped = mapper.Map<IEnumerable<AnimeGenreDTO>>(list);
            return mapped;
        }

        [HttpPost]
        public async Task<AnimeGenreDTO> CreateAG(AnimeGenreDTO animeGenreViewModel)
        {
            var mapped = mapper.Map<AnimeGenre>(animeGenreViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var entityDto = await animeGenreService.Create(mapped); 

            var mappedViewModel = mapper.Map<AnimeGenreDTO>(entityDto);
            return mappedViewModel;
        }


        [HttpDelete]
        public async Task DeleteAnimeGenre(int animeGenreId)
        {
            await animeGenreService.Delete(animeGenreId);
        }
    }
}
