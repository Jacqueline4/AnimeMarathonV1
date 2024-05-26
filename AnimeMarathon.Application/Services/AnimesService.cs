﻿using AnimeMarahon.Core.Entities;
using AnimeMarahon.Core.Repositories;
using AnimeMarathon.Application.DTOs;
using AnimeMarathon.Application.Interfaces;
using AnimeMarathon.Application.Services.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AnimeMarathon.Application.Services
{
    public class AnimesService : IAnimeService
    {
        private readonly IAnimeRepository animeRepository;
        private readonly IMapper _mapper;

        public AnimesService(IAnimeRepository animeRepository, IMapper mapper)
        {
            this.animeRepository = animeRepository;
            _mapper = mapper;
        }

        public async Task<AnimeDTO> Create(AnimeDTO animeDto)
        {
            await ValidateAnimeIfExist(animeDto);
            var anime = _mapper.Map<Anime>(animeDto);
            var newEntity = await animeRepository.AddAsync(anime);
            var newEntityDto = _mapper.Map<AnimeDTO>(newEntity);
            return newEntityDto;
        }

        public async Task Delete(int animeId)
        {
            //ValidateAnimeIfNotExist(anime);
            var deletedAnime = await animeRepository.GetByIdAsync(animeId);
            if (deletedAnime == null)
                throw new ApplicationException($"Entity could not be loaded.");

            await animeRepository.DeleteAsync(deletedAnime);
        }

        public async Task<AnimeDTO> GetAnimeById(int animeId)
        {
            var anime = await animeRepository.GetByIdAsync(animeId);
            return _mapper.Map<AnimeDTO>(anime); ;
        }

        public async Task<IEnumerable<AnimeDTO>> GetAnimeFilteredGenric(AnimeFilterDTO filtro)
        {
            var query = animeRepository.GetAllQueryable()
                .Include(x => x.AnimeGenres)
                    .ThenInclude(x => x.Genero)
                //.Include(x => x.AnimeRatings)
                .AsQueryable();

            if (filtro is null)
            {
                var all = await query.ToListAsync();
                return _mapper.Map<IEnumerable<AnimeDTO>>(all);
            }

            if (!string.IsNullOrEmpty(filtro.Nombre))
            {
                query = query.Where(x => x.Title.Contains(filtro.Nombre));
            }

            if (filtro.Desde.HasValue)
            {
                var desde = new DateTime(filtro.Desde.Value, 1, 1);
                query = query.Where(x => x.StartDate >= desde);
            }

            if (filtro.GeneroId.HasValue)
            { 
                query = query.Where(a => a.AnimeGenres.Select(g => g.GeneroId).Contains(filtro.GeneroId.Value));
            }

            if (filtro.Subtipo.HasValue)
            {
                query = query.Where(a => a.Subtype == filtro.GeneroId.Value.ToString()); //TODO subtipo = ENUM  =S quitar toString()
            }

            //TODO take & skip desde front
            var entities = await query.Take(10).Skip(0).ToListAsync();
            return _mapper.Map<IEnumerable<AnimeDTO>>(entities);
        }
        public async Task<IEnumerable<AnimeDTO>> GetAnimeByName(string animeName)
        {
            var animeList = await animeRepository.GetAnimeByNameAsync(animeName);
            return _mapper.Map<IEnumerable<AnimeDTO>>(animeList);
        }

        public async Task<IEnumerable<AnimeDTO>> GetAnimeByGenre(string genre)
        {
            var animeList = await animeRepository.GetAnimeByGenreAsync(genre);
            return _mapper.Map<IEnumerable<AnimeDTO>>(animeList);
        }

        public async Task<IEnumerable<AnimeDTO>> GetAnimeByUser(int userId)
        {
            try
            {
                var animeList = await animeRepository.GetAnimeByUserAsync(userId);
                var result = new List<AnimeDTO>();
                foreach(var anime in animeList)
                {
                    var model = _mapper.Map<AnimeDTO>(anime);
                    model.MiValoracion = anime.AnimeRatings.FirstOrDefault(x => x.UserId == userId)?.RatingVal;
                    result.Add(model);
                }
                return result;
                
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<AnimeDTO>();

            }
        }

        public async Task<IEnumerable<AnimeDTO>> GetAnimeList()
        {
            var animeList = await animeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AnimeDTO>>(animeList);
        }

        public async Task Update(AnimeDTO anime)
        {
            ValidateAnimeIfNotExist(anime);

            var editAnime = await animeRepository.GetByIdAsync(anime.Id);
            if (editAnime == null)
                throw new ApplicationException($"Entity could not be loaded.");

            editAnime.Id = anime.Id;
            editAnime.EndDate = anime.EndDate;
            editAnime.StartDate = anime.StartDate;
            editAnime.Description = anime.Description;
            editAnime.Title = anime.Title;
            editAnime.AverageRating = anime.AverageRating;
            editAnime.Status = anime.Status;
            editAnime.AgeRating = anime.AgeRating;
            editAnime.Subtype = anime.Subtype;
            editAnime.TotalEpisodes = anime.TotalEpisodes;
            editAnime.posterImage = anime.posterImage;

            await animeRepository.UpdateAsync(editAnime);
        }
        public async Task<IEnumerable<Comment>> GetCommentsByAnimeId(int animeId)
        {
            return await animeRepository.GetCommentsByAnimeId(animeId);
        }

        private async Task ValidateAnimeIfExist(AnimeDTO anime)
        {
            var existingEntity = await animeRepository.GetByIdAsync(anime.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{anime.ToString()} with this id already exists");
        }

        private void ValidateAnimeIfNotExist(AnimeDTO anime)
        {
            var existingEntity = animeRepository.GetByIdAsync(anime.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{anime.ToString()} with this id is not exists");
        }
    }
}
