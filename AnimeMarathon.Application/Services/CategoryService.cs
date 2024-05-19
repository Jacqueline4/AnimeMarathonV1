using AnimeMarahon.Core.Entities;
using AnimeMarahon.Core.Repositories;
using AnimeMarathon.Application.Interfaces;
using AnimeMarathon.Application.Services.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryResository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryResository, IMapper mapper)
        {
            this.categoryResository = categoryResository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryDTO>> GetCategoryList()
        {
            var categoryList = await categoryResository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categoryList);

        }
        public async Task<IEnumerable<CategoryDTO>> GetCategoryByName(string category)
        {
            var categoryList = await categoryResository.GetCategoryByAnimeNameAsync(category);
            return _mapper.Map<IEnumerable<CategoryDTO>>(categoryList);
        }
        public async Task<CategoryDTO> Create(CategoryDTO categoryDto)
        {
            await ValidateCategoryIfExist(categoryDto);
            var category = _mapper.Map<Category>(categoryDto);

            var newEntity = await categoryResository.AddAsync(category);
            return _mapper.Map<CategoryDTO>(newEntity); ;
        }
        public async Task Update(CategoryDTO category)
        {
            ValidateCategoryIfNotExist(category);

            var editCategory = await categoryResository.GetByIdAsync(category.Id);
            if (editCategory == null)
                throw new ApplicationException($"Entity could not be loaded.");

            editCategory.Id = category.Id;
            editCategory.Description = category.Description;
            editCategory.Name = category.Name;

            await categoryResository.UpdateAsync(editCategory);

        }
        public async Task Delete(int categoryId)
        {
            var deletedCategory = await categoryResository.GetByIdAsync(categoryId);
            if (deletedCategory == null)
                throw new ApplicationException($"Entity could not be loaded.");

            await categoryResository.DeleteAsync(deletedCategory);
        }

        private async Task ValidateCategoryIfExist(CategoryDTO category)
        {
            var existingEntity = await categoryResository.GetByIdAsync(category.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{category.ToString()} with this id already exists");
        }

        private void ValidateCategoryIfNotExist(CategoryDTO category)
        {
            var existingEntity = categoryResository.GetByIdAsync(category.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{category.ToString()} with this id is not exists");
        }
    }
}
