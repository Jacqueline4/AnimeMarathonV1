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
    public class AnimeRepository : Repository<Anime>, IAnimeRepository
    {
        public AnimeRepository(AnimeMarathonContext dbContext) : base(dbContext) 
        {
        }
        public async Task<IEnumerable<Anime>> GetAnimeByNameAsync(string animeName)
        {
            return await dbContext.Animes
                .Where(x => x.Title.Contains(animeName))
                .ToListAsync();
        }
        //public async Task<IEnumerable<Anime>> GetAnimeByGenreAsync(int genreId)
        //{
        //    return await dbContext.Animes
        //        .Where(x => x.GenreId.Equals(genreId))
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<Anime>> GetAnimeByRating(decimal rating)
        {
            return await dbContext.Animes
                .Where(x => x.AverageRating.Equals(rating))
                .ToListAsync();
        }

        //TODO  byEditorial, byTags/byTematicas, byAutor, byStudio, byAñoEstreno
    }
}
