using AnimeMarahon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Application.Interfaces
{
    public interface IAnimeGenreService
    {
        Task<IEnumerable<AnimeGenre>> GetAnimeGenreList();
        Task<AnimeGenre> Create(AnimeGenre ag);
        Task Delete(int agId);
    }
}
