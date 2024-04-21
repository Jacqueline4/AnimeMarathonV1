using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.Interfaces;
using AnimeMarathonV1.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeMarathonV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAnimeService animeService;
        public AnimeController(IMapper mapper, IAnimeService animeService )
        {
            this.mapper= mapper;
            this.animeService = animeService;
        }

        [HttpGet("GetAnimeByName/{animeName}")]
        public async Task<IEnumerable<AnimeDTO>> GetAnimes(string animeName)
        {
            if (string.IsNullOrWhiteSpace(animeName))
            {
                var list = await animeService.GetAnimeList();
                var mapped = mapper.Map<IEnumerable<AnimeDTO>>(list);
                return mapped;
            }

            var listByName = await animeService.GetAnimeByName(animeName);
            var mappedByName = mapper.Map<IEnumerable<AnimeDTO>>(listByName);
            return mappedByName;
        }

        [HttpGet("GetAnimeById/{animeId}")]
        public async Task<AnimeDTO> GetAnimeById(int animeId)
        {
            var anime = await animeService.GetAnimeById(animeId);
            var mapped = mapper.Map<AnimeDTO>(anime);
            return mapped;
        }

        [HttpPost]
        public async Task<AnimeDTO> CreateAnime(AnimeDTO animeViewModel)
        {
            var mapped = mapper.Map<Anime>(animeViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var entityDto = await animeService.Create(mapped);

            var mappedViewModel = mapper.Map<AnimeDTO>(entityDto);
            return mappedViewModel;
        }

        [HttpPut]
        public async Task UpdateAnime(AnimeDTO animeViewModel)
        {
            var mapped = mapper.Map<Anime>(animeViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            await animeService.Update(mapped);
        }

        [HttpDelete]
        public async Task DeleteAnime(AnimeDTO animeViewModel)
        {
            var mapped = mapper.Map<Anime>(animeViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            await animeService.Delete(mapped);
        }
    }
}
