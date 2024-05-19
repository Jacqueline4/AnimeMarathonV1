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
                .Where(x => x.Title.Contains(animeName))
                .ToListAsync();
        }
        public async Task<IEnumerable<Anime>> GetAnimeByGenreAsync(string genreName)
        {

            // Primero, obtenemos el ID del género basado en su nombre
            var genre = await dbContext.Genres.FirstOrDefaultAsync(g => g.Name.Contains(genreName));
            if (genre == null)
            {
                // Si el género no existe, retornamos una lista vacía
                return new List<Anime>();
            }

            // Luego, buscamos los IDs de los animes que están asociados con este género
            var animeIds = await dbContext.AnimeGenre
                                          .Where(ag => ag.GeneroId == genre.Id)
                                          .Select(ag => ag.AnimeId)
                                          .ToListAsync();

            // Finalmente, obtenemos los animes que tienen los IDs encontrados
            return await dbContext.Animes
                                    .Where(a => animeIds.Equals(a.Id))
                                    .ToListAsync();
        }
        public async Task<IEnumerable<Anime>> GetAnimeByRating(decimal rating)
        {
            return await dbContext.Animes
                .Where(x => x.AverageRating.Equals(rating))
                .ToListAsync();
        }
        public async Task<IEnumerable<Anime>> GetAnimeByCategoryAsync(string categoryName)
        {
            var category = await dbContext.Categories.FirstOrDefaultAsync(g => g.Name.Contains(categoryName));
            if (category == null)
            {
                return new List<Anime>();
            }
            var animeIds = await dbContext.AnimeCategories
                                          .Where(ac => ac.CategoriaId == category.Id)
                                          .Select(ac => ac.AnimeId)
                                          .ToListAsync();

            return await dbContext.Animes
                                    .Where(a => animeIds.Equals(a.Id))
                                    .ToListAsync();
        }
        public async Task<IEnumerable<Anime>> GetAnimeByAgeRatingAsync(string ageRating)
        {
            return await dbContext.Animes
                 .Where(x => x.AgeRating.Contains(ageRating))
                 .ToListAsync();
        }
        public async Task<IEnumerable<Anime>> GetAnimeByStatusAsync(string status)
        {
            return await dbContext.Animes
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
           
            var animeIds = await dbContext.UsersAnimes
                                          .Where(ac => ac.UsuarioId == userId)
                                          .Select(ac => ac.AnimeId)
                                          .ToListAsync();

            return await dbContext.Animes
                                    .Where(a => animeIds.Contains(a.Id))
                                    .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByAnimeId(int animeId)
        {
            var comments = await dbContext.Comments.Where(c => c.AnimeId == animeId).ToListAsync();
            return comments;
        }

       

    }
}
