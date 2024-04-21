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
       // Task<IEnumerable<Anime>> GetAnimeByGenreAsync(int genreId);
        Task<IEnumerable<Anime>> GetAnimeByRating(decimal rating);

    }
}
