using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Application.Interfaces
{
    public interface IAnimeService
    {
        Task<IEnumerable<AnimeDTO>> GetAnimeList();
        Task<AnimeDTO> GetAnimeById(int animeId);
        Task<IEnumerable<AnimeDTO>> GetAnimeByName(string anime);
        Task<AnimeDTO> Create(AnimeDTO anime);
        Task Update(AnimeDTO anime);
        //Task Delete(Anime anime);
        Task Delete (int animeId);
        Task<IEnumerable<AnimeDTO>> GetAnimeByUser(int userId);
        Task<IEnumerable<Comment>> GetCommentsByAnimeId(int animeId);
        
    }
}
