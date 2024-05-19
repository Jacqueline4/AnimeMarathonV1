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
        public async Task<IEnumerable<Category>> GetCategoryByAnimeNameAsync(string animeName)
        {
            var anime = await dbContext.Animes.FirstOrDefaultAsync(g => g.Title.Contains(animeName));
            if (anime == null)
            {
                return new List<Category>();
            }

            var categoriesIds = await dbContext.AnimeCategories
                                          .Where(ag => ag.AnimeId == anime.Id)
                                          .Select(ag => ag.CategoriaId)
                                          .ToListAsync();

            return await dbContext.Categories
                                    .Where(a => categoriesIds.Equals(a.Id))
                                    .ToListAsync();
        }
    }
    
}
