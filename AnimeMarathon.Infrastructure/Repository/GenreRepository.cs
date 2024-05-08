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
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(AnimeMarathonContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Genre>> GetGenreByAnimeNameAsync(string animeName)
        {

            //var anime = await dbContext.Animes.FirstOrDefaultAsync(g => g.Title.Contains(animeName));
            //if (anime == null)
            //{
            //    return new List<Genre>();
            //}

            //var genresIds = await dbContext.AnimeGenre
            //                              .Where(ag => ag.AnimeId == anime.Id)
            //                              .Select(ag => ag.GenreId)
            //                              .ToListAsync();

            //return await dbContext.Genres
            //                        .Where(a => genresIds.Contains(a.Id))
            //                        .ToListAsync();

           var genres = await dbContext.Genres
     .Join(dbContext.AnimeGenre,
         genre => genre.Id,
         animeGenre => animeGenre.GeneroId,
         (genre, animeGenre) => new { Genre = genre, AnimeGenre = animeGenre })
     .Join(dbContext.Animes,
         ag => ag.AnimeGenre.AnimeId,
         anime => anime.Id,
         (ag, anime) => new { Genre = ag.Genre, Anime = anime })
     .Where(agAnime => agAnime.Anime.Title.Contains(animeName))
     .Select(agAnime => agAnime.Genre)
     .ToListAsync();

 return genres;


        }
    }
}
