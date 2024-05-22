using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Application.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDTO>> GetGenreList();
        Task<IEnumerable<GenreDTO>> GetGenresByAnimeId(int animeId);
        Task<GenreDTO> Create(GenreDTO genre);
        Task Update(GenreDTO genre);
        Task Delete(int genreId);
    }
}
