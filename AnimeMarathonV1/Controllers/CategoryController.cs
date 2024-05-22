using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.Interfaces;
using AnimeMarathon.Application.Services.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeMarathonV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;
        public CategoryController(IMapper mapper, ICategoryService categoryService)
        {
            this.mapper = mapper;
            this.categoryService = categoryService;
        }

        [HttpGet("GetCategoryByAnimeId/{animeId}")]
        public async Task<IEnumerable<CategoryDTO>> GetCategoryByAnimeId(int animeId)
        {
            //if (string.IsNullOrWhiteSpace(categoryName))
            //{
            //    var list = await categoryService.GetCategoryList(); 
            //    var mapped = mapper.Map<IEnumerable<CategoryDTO>>(list);
            //    return mapped;
            //}

            var listByName = await categoryService.GetCategoryByAnimeId(animeId); 
            var mappedByName = mapper.Map<IEnumerable<CategoryDTO>>(listByName);
            return mappedByName;
        }
        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> Get()
        {      
                var list = await categoryService.GetCategoryList();
                var mapped = mapper.Map<IEnumerable<CategoryDTO>>(list);
                return mapped;
        }

        [HttpPost]
        public async Task<CategoryDTO> CreateGenre(CategoryDTO categoryViewModel)
        {
            var mapped = mapper.Map<CategoryDTO>(categoryViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var entityDto = await categoryService.Create(mapped);

            var mappedViewModel = mapper.Map<CategoryDTO>(entityDto);
            return mappedViewModel;
        }

        [HttpPut]
        public async Task UpdateCategory(CategoryDTO categoryViewModel)
        {
            var mapped = mapper.Map<CategoryDTO>(categoryViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            await categoryService.Update(mapped);
        }

        [HttpDelete]
        public async Task DeleteCategory(int categoryId)
        {

            await categoryService.Delete(categoryId);
        }
    }
}
