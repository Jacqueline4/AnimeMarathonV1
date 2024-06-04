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
using System.Xml.Linq;

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
                .Include(x => x.UsersAnime)
                //.ThenInclude(x => x.Comments)
                .AsNoTracking()
                .Where(x => x.Title.Contains(animeName))
                .ToListAsync();
        }
        public async Task<IEnumerable<Anime>> GetAnimeByCategoryAsync(string categoryName)
        {

            // Primero, obtenemos el ID de la categoria basado en su nombre
            var category = await dbContext.Categories
                .Include(x => x.AnimesCategory).ThenInclude(ac => ac.Anime)
                .AsNoTracking()
                .FirstOrDefaultAsync(cat => cat.Name.Contains(categoryName));

            if (category is not null)
            {
                //mapeo de animes
                return category.AnimesCategory.Select(ac => ac.Anime);
            }

            return Enumerable.Empty<Anime>();
        }
        public async Task<IEnumerable<Anime>> GetAnimeByGenreAsync(string genreName)
        {

            // Primero, obtenemos el ID del género basado en su nombre
            var genre = await dbContext.Genres
                .Include(x => x.AnimesGenre).ThenInclude(ag => ag.Anime)
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Name.Contains(genreName));

            if (genre is not null)
            {
                //mapeo de animes
                return genre.AnimesGenre.Select(ag => ag.Anime);
            }

            return Enumerable.Empty<Anime>();
        }

        public async Task<IEnumerable<Anime>> GetAnimeByRating(decimal rating)
        {
            return await dbContext.Animes.AsNoTracking()
                .Where(x => x.AverageRating.Equals(rating))
                .ToListAsync();
        }



        //public async Task<IEnumerable<Anime>> GetAnimeByCategoryAsync(string categoryName)
        //{
        //    var category = await dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(g => g.Name.Contains(categoryName));
        //    if (category == null)
        //    {
        //        return new List<Anime>();
        //    }
        //    var animeIds = await dbContext.AnimeCategories
        //                                  .Where(ac => ac.CategoriaId == category.Id)
        //                                  .Select(ac => ac.AnimeId)
        //                                  .ToListAsync();

        //    return await dbContext.Animes
        //                            .Where(a => animeIds.Equals(a.Id))
        //                            .ToListAsync();
        //}
        public async Task<IEnumerable<Anime>> GetAnimeByAgeRatingAsync(string ageRating)
        {
            return await dbContext.Animes
                 .Where(x => x.AgeRating.Contains(ageRating))
                 .ToListAsync();
        }
        public async Task<IEnumerable<Anime>> GetAnimeByStatusAsync(string status)
        {
            return await dbContext.Animes.AsNoTracking()
                .Where(x => x.Status.Equals(status))
                .ToListAsync();
        }
        public async Task<IEnumerable<Anime>> GetAnimeBySubtypeAsync(string subtype)
        {
            return await dbContext.Animes
                .Where(x => x.Subtype.Equals(subtype))
                .ToListAsync();
        }
        public async Task<IEnumerable<Anime>> GetAnimeByUserAsync(int userId)
        {
            var animes = await dbContext.Animes
                                    .Include(x => x.UsersAnime)
                                      //  .ThenInclude(x => x.UsuarioId)
                                    .Include(x => x.AnimeRatings)
                                    //.Include(x => x.AnimeGenres).ThenInclude(x => x.Genero)
                                    .AsNoTracking()
                                    .Where(a => a.UsersAnime.Select(ua => ua.UsuarioId).Contains(userId)).ToListAsync();

            return animes ?? Enumerable.Empty<Anime>();
            //var animeIds = await dbContext.UsersAnimes
            //                              .Where(ac => ac.UsuarioId == userId)
            //                              .Select(ac => ac.AnimeId)
            //                              .ToListAsync();

            //return await dbContext.Animes
            //                        .Where(a => animeIds.Contains(a.Id))
            //                        .ToListAsync();
        }
        public async Task<IEnumerable<Comment>> GetCommentsByAnimeId(int animeId)
        {
            var comments = await dbContext.Comments
                                    .Include(x => x.User)
                                    .Where(c => c.AnimeId == animeId).ToListAsync();
            return comments;
        }



    }
}
