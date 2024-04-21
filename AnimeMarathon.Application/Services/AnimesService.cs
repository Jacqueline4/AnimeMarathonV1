using AnimeMarahon.Core.Entities;
using AnimeMarahon.Core.Repositories;
using AnimeMarathon.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Application.Services
{
    public class AnimesService : IAnimeService
    {
        private readonly IAnimeRepository animeRepository;

        public AnimesService(IAnimeRepository animeRepository)
        {
            this.animeRepository = animeRepository;
        }

        public async Task<Anime> Create(Anime anime)
        {
            await ValidateAnimeIfExist(anime);

            var newEntity = await animeRepository.AddAsync(anime);
            return newEntity;
        }

        public async Task Delete(Anime anime)
        {
            ValidateAnimeIfNotExist(anime);
            var deletedAnime = await animeRepository.GetByIdAsync(anime.Id);
            if (deletedAnime == null)
                throw new ApplicationException($"Entity could not be loaded.");

            await animeRepository.DeleteAsync(deletedAnime);
        }

        public async Task<Anime> GetAnimeById(int animeId)
        {
            var anime = await animeRepository.GetByIdAsync(animeId);
            return anime;
        }

        public async Task<IEnumerable<Anime>> GetAnimeByName(string animeName)
        {
            var animeList = await animeRepository.GetAnimeByNameAsync(animeName);
            return animeList;
        }

        public async Task<IEnumerable<Anime>> GetAnimeList()
        {
            var animeList = await animeRepository.GetAllAsync();
            return animeList;
        }

        public async Task Update(Anime anime)
        {
            ValidateAnimeIfNotExist(anime);

            var editAnime = await animeRepository.GetByIdAsync(anime.Id);
            if (editAnime == null)
                throw new ApplicationException($"Entity could not be loaded.");

            editAnime.Id= anime.Id;
            editAnime.EndDate = anime.EndDate;
            editAnime.StartDate= anime.StartDate;
            editAnime.Description = anime.Description; 
            editAnime.Title = anime.Title;
            editAnime.AverageRating = anime.AverageRating;
           // editAnime.GenreId = anime.GenreId;

            await animeRepository.UpdateAsync(editAnime);
        }

        private async Task ValidateAnimeIfExist(Anime anime)
        {
            var existingEntity = await animeRepository.GetByIdAsync(anime.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{anime.ToString()} with this id already exists");
        }

        private void ValidateAnimeIfNotExist(Anime anime)
        {
            var existingEntity = animeRepository.GetByIdAsync(anime.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{anime.ToString()} with this id is not exists");
        }
    }
}
