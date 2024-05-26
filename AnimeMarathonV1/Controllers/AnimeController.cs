using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.DTOs;
using AnimeMarathon.Application.Interfaces;
using AnimeMarathon.Application.Interfaces.Base;
using AnimeMarathon.Application.Services;
using AnimeMarathon.Application.Services.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AnimeMarathonV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAnimeService animeService;
        private readonly IBaseServices<UsersAnimeDTO,UsersAnimes> baseServices;
        public AnimeController(IMapper mapper, IAnimeService animeService, IBaseServices<UsersAnimeDTO,UsersAnimes> baseServices)
        {
            this.mapper= mapper;
            this.animeService = animeService;
            this.baseServices = baseServices;
        }
        [HttpGet]
        public async Task<IEnumerable<AnimeDTO>> Get()
        {
            var list = await animeService.GetAnimeList();   
            var mapped = mapper.Map<IEnumerable<AnimeDTO>>(list);
            return mapped;
        
        }
        [HttpPost("GetAnimes")]
        public async Task<IEnumerable<AnimeDTO>> GetAnimesPost([FromBody] AnimeFilterDTO data)
        {  
             
            var listByName = await animeService.GetAnimeFilteredGenric(data);
            var mappedByName = mapper.Map<IEnumerable<AnimeDTO>>(listByName);
            return mappedByName;
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

        [HttpGet("GetAnimeByUserId/{userId}")]
        public async Task<IEnumerable<AnimeDTO>> GetAnimeByUserId(int userId)
        {
            try
            {
                var animes = await animeService.GetAnimeByUser(userId);
                return animes;
            }
            catch
            {
                return Enumerable.Empty<AnimeDTO>();
            }
        }


        [HttpGet("GetAnimesByGenre/{genre}")]
        public async Task<IEnumerable<AnimeDTO>> GetAnimesByGenre(string genre)
        {
            var anime = await animeService.GetAnimeByGenre(genre);
            //var mapped = mapper.Map<AnimeDTO>(anime);
            return anime;
        }

        [HttpGet("GetCommentByAnime/{animeId}")]
        public async Task<IEnumerable<CommentDTO>> GetCommentsByAnimeId(int animeId)
        {
            var comments = await animeService.GetCommentsByAnimeId(animeId);
            var mapped = mapper.Map<IEnumerable<CommentDTO>>(comments);
            return mapped;
        }


        [HttpPost ("UserAnime")]
        public async Task<UsersAnimeDTO> CreateUA(UsersAnimeDTO ViewModel)
        {
            var mapped = mapper.Map<UsersAnimes>(ViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var entityDto = await baseServices.Create(ViewModel);

            var mappedViewModel = mapper.Map<UsersAnimeDTO>(entityDto);
            return mappedViewModel;
        }


        [HttpPost]
        public async Task<AnimeDTO> CreateAnime(AnimeDTO animeViewModel)
        {
            var mapped = mapper.Map<Anime>(animeViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var entityDto = await animeService.Create(animeViewModel);

            var mappedViewModel = mapper.Map<AnimeDTO>(entityDto);
            return mappedViewModel;
        }

        [HttpPut]
        public async Task UpdateAnime(AnimeDTO animeViewModel)
        {
            var mapped = mapper.Map<Anime>(animeViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            await animeService.Update(animeViewModel);
        }

        [HttpDelete]
        public async Task DeleteAnime(int animeId)
        {
            //var mapped = mapper.Map<Anime>(animeViewModel);
            //if (mapped == null)
            //    throw new Exception($"Entity could not be mapped.");

            await animeService.Delete(animeId);
        }
    }
}
