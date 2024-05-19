using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.Interfaces;
using AnimeMarathon.Application.Interfaces.Base;
using AnimeMarathon.Application.Services.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeMarathonV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimeGenreController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBaseServices<AnimeGenreDTO,AnimeGenre> baseServices;
        public AnimeGenreController(IMapper mapper, IBaseServices<AnimeGenreDTO,AnimeGenre> baseServices) 
        {
            this.mapper = mapper;
            this.baseServices = baseServices;
        }
        [HttpGet]
        public async Task<IEnumerable<AnimeGenreDTO>> Get()
        {
            var list = await baseServices.GetList();    
            var mapped = mapper.Map<IEnumerable<AnimeGenreDTO>>(list);
            return mapped;
        }

        [HttpPost]
        public async Task<AnimeGenreDTO> CreateAG(AnimeGenreDTO animeGenreViewModel)
        {
            var mapped = mapper.Map<AnimeGenre>(animeGenreViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var entityDto = await baseServices.Create(animeGenreViewModel); 

            var mappedViewModel = mapper.Map<AnimeGenreDTO>(entityDto);
            return mappedViewModel;
        }


        [HttpDelete]
        public async Task DeleteAnimeGenre(int animeGenreId)
        {
            await baseServices.Delete(animeGenreId);
        }
    }
}
