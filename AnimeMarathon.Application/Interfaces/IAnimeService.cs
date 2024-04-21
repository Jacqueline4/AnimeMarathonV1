using AnimeMarahon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Application.Interfaces
{
    public interface IAnimeService
    {
        Task<IEnumerable<Anime>> GetAnimeList();
        Task<Anime> GetAnimeById(int animeId);
        Task<IEnumerable<Anime>> GetAnimeByName(string anime);
        Task<Anime> Create(Anime anime);
        Task Update(Anime anime);
        Task Delete(Anime anime);
    }
}
