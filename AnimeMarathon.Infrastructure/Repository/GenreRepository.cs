using AnimeMarahon.Core.Entities;
using AnimeMarahon.Core.Repositories;
using AnimeMarathon.Data.Data;
using AnimeMarathon.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Data.Repository
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(AnimeMarathonContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Genre>> GetGenreByAnimeIdAsync(int animeId)
        {
            var genres = await dbContext.Genres
        .Join(dbContext.AnimeGenre,
            genre => genre.Id,
            animeGenre => animeGenre.GeneroId,
            (genre, animeGenre) => new { Genre = genre, AnimeGenre = animeGenre })
        .Where(ag => ag.AnimeGenre.AnimeId == animeId)
        .Select(ag => ag.Genre)
        .ToListAsync();

            return genres;


        }

        public  async Task<IEnumerable<Genre>> GetAllWithAnimesAsync()
        {
            var genres = await dbContext.Genres.Include(x => x.AnimesGenre).ThenInclude(ag => ag.Anime).AsNoTracking().ToListAsync();
            return genres; 
        }
    }
}
