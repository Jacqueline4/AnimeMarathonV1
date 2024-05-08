using AnimeMarahon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Application.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetGenreList();
        Task<IEnumerable<Genre>> GetGenresByAnimeName(string genre);
        Task<Genre> Create(Genre genre);
        Task Update(Genre genre);
        Task Delete(int genreId);
    }
}
