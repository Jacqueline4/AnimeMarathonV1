using AnimeMarahon.Core.Entities;
using AnimeMarahon.Core.Repositories;
using AnimeMarathon.Application.Interfaces;
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
        public CategoryService(ICategoryRepository categoryResository) 
        { 
            this.categoryResository = categoryResository;   
        }
        public async Task<IEnumerable<Category>> GetCategoryList()
        {
            var categoryList = await categoryResository.GetAllAsync();
            return categoryList;

        }
        public async Task<IEnumerable<Category>> GetCategoryByName(string category)
        {
            var categoryList = await categoryResository.GetCategoryByAnimeNameAsync(category);
            return categoryList;
        }
        public async Task<Category> Create(Category category)
        {
            await ValidateCategoryIfExist(category);

            var newEntity = await categoryResository.AddAsync(category);
            return newEntity;
        }
        public async Task Update(Category category)
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

        private async Task ValidateCategoryIfExist(Category category)
        {
            var existingEntity = await categoryResository.GetByIdAsync(category.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{category.ToString()} with this id already exists");
        }

        private void ValidateCategoryIfNotExist(Category category)
        {
            var existingEntity = categoryResository.GetByIdAsync(category.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{category.ToString()} with this id is not exists");
        }
    }
}
