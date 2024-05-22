using AnimeMarahon.Core.Entities;
using AnimeMarahon.Core.Repositories;
using AnimeMarathon.Application.Interfaces;
using AnimeMarathon.Application.Services.DTOs;
using AutoMapper;
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

        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            this.genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreDTO>> GetGenreList()
        {
            var genreList = await genreRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GenreDTO>>(genreList); 
        }
        public async Task<IEnumerable<GenreDTO>> GetGenresByAnimeId(int animeId)
        {
            var genreList = await genreRepository.GetGenreByAnimeIdAsync(animeId);
            return _mapper.Map<IEnumerable<GenreDTO>>(genreList);
        }
        public async Task<GenreDTO> Create(GenreDTO genreDto)
        {
            await ValidateGenreIfExist(genreDto);

            var genre = _mapper.Map<Genre>(genreDto);
            var newEntity = await genreRepository.AddAsync(genre);
            var newEntityDto = _mapper.Map<GenreDTO>(newEntity);
            return newEntityDto;
        }
        public async Task Update(GenreDTO genre)
        {
            ValidateGenreIfNotExist(genre);

            var editGenre = await genreRepository.GetByIdAsync(genre.Id);
            if (editGenre == null)
                throw new ApplicationException($"Entity could not be loaded.");

            editGenre.Id = genre.Id;
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
        private async Task ValidateGenreIfExist(GenreDTO genre)
        {
            var existingEntity = await genreRepository.GetByIdAsync(genre.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{genre.ToString()} with this id already exists");
        }

        private void ValidateGenreIfNotExist(GenreDTO genre)
        {
            var existingEntity = genreRepository.GetByIdAsync(genre.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{genre.ToString()} with this id is not exists");
        }
    }
}
