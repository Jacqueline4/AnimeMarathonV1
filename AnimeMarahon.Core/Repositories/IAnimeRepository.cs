using AnimeMarahon.Core.Entities;
using AnimeMarahon.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarahon.Core.Repositories
{
    public interface IAnimeRepository :IRepository<Anime>
    {
        Task<IEnumerable<Anime>> GetAnimeByNameAsync(string animeName);
        Task<IEnumerable<Anime>> GetAnimeByGenreAsync(string genreName);
        Task<IEnumerable<Anime>> GetAnimeByRating(decimal rating);
        Task<IEnumerable<Anime>> GetAnimeByCategoryAsync(string categoryName);
        Task<IEnumerable<Anime>> GetAnimeByAgeRatingAsync(string ageRating);
        Task<IEnumerable<Anime>> GetAnimeByStatusAsync(string status);
        Task<IEnumerable<Anime>> GetAnimeBySubtypeAsync(string subtype);
        Task<IEnumerable<Anime>> GetAnimeByUserAsync(int id);

    }
}
