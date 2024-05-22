using AnimeMarahon.Core.Entities;
using AnimeMarahon.Core.Repositories;
using AnimeMarathon.Data.Data;
using AnimeMarathon.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AnimeMarathonContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Category>> GetCategoryByAnimeIdAsync(int animeId)
        {
            var categories = await dbContext.Categories
  .Join(dbContext.AnimeCategories,
      category => category.Id,
      animeCategory => animeCategory.CategoriaId,
      (category, animeCategory) => new { Category = category, AnimeCategory = animeCategory })
  .Where(ag => ag.AnimeCategory.AnimeId == animeId)
  .Select(ag => ag.Category)
  .ToListAsync();

            return categories;

        }
    }
    
}
