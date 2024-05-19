using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.Interfaces;
using AnimeMarathon.Application.Interfaces.Base;
using AnimeMarathon.Application.Services;
using AnimeMarathon.Application.Services.Base;
using AnimeMarathon.Application.Services.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeMarathonV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimeCategoryController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBaseServices<AnimeCategoryDTO,AnimeCategory> baseServices ;
        public AnimeCategoryController(IMapper mapper, IBaseServices<AnimeCategoryDTO,AnimeCategory> baseServices)
        {
            this.mapper = mapper;
            this.baseServices= baseServices;
        }
        [HttpGet]
        public async Task<IEnumerable<AnimeCategoryDTO>> Get()
        {
            var list = await baseServices.GetList();
            var mapped = mapper.Map<IEnumerable<AnimeCategoryDTO>>(list);
            return mapped;
        }

        [HttpPost]
        public async Task<AnimeCategoryDTO> Create(AnimeCategoryDTO ViewModel)
        {
            var mapped = mapper.Map<AnimeCategory>(ViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var entityDto = await baseServices.Create(ViewModel);

            var mappedViewModel = mapper.Map<AnimeCategoryDTO>(entityDto);
            return mappedViewModel;
        }

        [HttpDelete]
        public async Task Delete(int id)
        {

            await baseServices.Delete(id);
        }
    }
}
