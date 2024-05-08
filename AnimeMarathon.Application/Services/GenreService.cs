using AnimeMarahon.Core.Entities;
using AnimeMarahon.Core.Repositories;
using AnimeMarathon.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AnimeMarathon.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository genreRepository;

        public GenreService (IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        public async Task<IEnumerable<Genre>> GetGenreList()
        {
            var genreList = await genreRepository.GetAllAsync();
            return genreList;
        }
        public async Task<IEnumerable<Genre>> GetGenresByAnimeName(string animeName)
        {
            var genreList = await genreRepository.GetGenreByAnimeNameAsync(animeName);
            return genreList;
        }
        public async Task<Genre> Create(Genre genre)
        {
            await ValidateGenreIfExist(genre);

            var newEntity = await genreRepository.AddAsync(genre);
            return newEntity;
        }
        public async Task Update(Genre genre)
        {
            ValidateGenreIfNotExist(genre);

            var editGenre = await genreRepository.GetByIdAsync(genre.Id);
            if (editGenre == null)
                throw new ApplicationException($"Entity could not be loaded.");

            editGenre.Id = genre.Id;
            editGenre.Description = genre.Description;
            editGenre.Name = genre.Name;          

            await genreRepository.UpdateAsync(editGenre);
        }
        public async Task Delete(int genreId)
        {
            var deletedGenre = await genreRepository.GetByIdAsync(genreId);
            if (deletedGenre == null)
                throw new ApplicationException($"Entity could not be loaded.");

            await genreRepository.DeleteAsync(deletedGenre);
        }
        private async Task ValidateGenreIfExist(Genre genre)
        {
            var existingEntity = await genreRepository.GetByIdAsync(genre.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{genre.ToString()} with this id already exists");
        }

        private void ValidateGenreIfNotExist(Genre genre)
        {
            var existingEntity = genreRepository.GetByIdAsync(genre.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{genre.ToString()} with this id is not exists");
        }
    }
}
