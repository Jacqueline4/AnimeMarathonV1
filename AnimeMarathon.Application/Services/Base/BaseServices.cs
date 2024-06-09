using AnimeMarahon.Core.Entities;
using AnimeMarahon.Core.Entities.Base;
using AnimeMarahon.Core.Repositories.Base;
using AnimeMarathon.Application.Interfaces.Base;
using AnimeMarathon.Application.Services.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AnimeMarathon.Application.Services.Base
    {
    public class BaseServices<TDto, TEntity> : IBaseServices<TDto,TEntity>
    where TDto : BaseDTO
    where TEntity : BaseEntity
    {
        private readonly IRepository<TEntity> repository;
        private readonly IMapper _mapper;

        public BaseServices(IRepository<TEntity> repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }
        public virtual async Task<TDto> Update(TDto entityDto)
        {
            if (entityDto == null)
            {
                throw new ArgumentNullException(nameof(entityDto), "The entity DTO cannot be null.");
            }

            // Map the DTO to the entity
            TEntity entity = _mapper.Map<TEntity>(entityDto);

            // Update the entity in the repository asynchronously
            TEntity updatedEntity = await repository.UpdateAsync(entity);

            // Map the updated entity back to a DTO
            TDto updatedEntityDto = _mapper.Map<TDto>(updatedEntity);

            // Return the updated DTO
            return updatedEntityDto;

        }

        public virtual async Task<TDto> Create(TDto entityDto)
        {
            if (entityDto is AnimeGenreDTO)
            {
                var animeGenre = entityDto as AnimeGenreDTO;
                animeGenre.Id = GenerateCompositeId(animeGenre.AnimeId, animeGenre.GeneroId);
            }
            else if (entityDto is AnimeCategoryDTO)
            {
                var animeCategory = entityDto as AnimeCategoryDTO;
                animeCategory.Id = GenerateCompositeId(animeCategory.AnimeId, animeCategory.CategoriaId);
            }
            else if (entityDto is UsersAnimeDTO)
            {
                var userAnime = entityDto as UsersAnimeDTO;
                userAnime.Id = GenerateCompositeId(userAnime.AnimeId, userAnime.UsuarioId);
            }
           

            TEntity entity = _mapper.Map<TEntity>(entityDto);
            TEntity newEntity = await repository.AddAsync(entity);
            TDto newEntityDto = _mapper.Map<TDto>(newEntity);
            return newEntityDto;
        }

        public async Task<IEnumerable<TDto>> GetList()
        {
            IEnumerable<TEntity> entities = await repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task Delete(int id)
        {
            TEntity entity = await repository.GetByIdAsync(id);
            if (entity == null)
                throw new ApplicationException($"Entity with id {id} not found.");

            await repository.DeleteAsync(entity);
        }

        protected virtual async Task ValidateEntityIfNotExist(TDto entityDto)
        {
            TEntity entity = _mapper.Map<TEntity>(entityDto);
            TEntity existingEntity = await repository.GetByIdAsync(entity.Id);
            if (existingEntity != null)
                throw new ApplicationException($"Entity with id {entity.Id} already exists.");
        }

        protected int GenerateCompositeId(int firstId, int secondId)
        {
            string formattedFirstId = firstId.ToString("D4");
            string formattedSecondId = secondId.ToString("D4");
            string idString = $"1{formattedFirstId}{formattedSecondId}";

            if (int.TryParse(idString, out int id))
            {
                return id;
            }
            else
            {
                throw new ArgumentException("No se pudo generar el ID compuesto como un entero.");
            }
        }
    }
    //public class BaseServices<T> : IBaseServices<T> where T : BaseDTO
    //{
    //    private readonly IRepository<T> repository;
    //private readonly IMapper _mapper;


    //public BaseServices(IRepository<T> repository, IMapper mapper)
    //    {
    //        this.repository = repository;
    //    _mapper = mapper;
    //}


    //        public virtual async Task<T> Create(T entityDto)
    //        {
    //            if (entityDto is AnimeGenre)
    //            {
    //                var animeGenre = entityDto as AnimeGenre;
    //                animeGenre.Id = GenerateCompositeId(animeGenre.AnimeId, animeGenre.GeneroId);
    //            }
    //            else if (entityDto is AnimeCategory)
    //            {
    //                var animeCategory = entityDto as AnimeCategory;
    //                animeCategory.Id = GenerateCompositeId(animeCategory.AnimeId, animeCategory.CategoriaId);
    //            }
    //            else if (entityDto is UsersAnimes)
    //            {
    //                var userAnime = entityDto as UsersAnimes;
    //                userAnime.Id = GenerateCompositeId(userAnime.AnimeId, userAnime.UsuarioId);
    //            }
    //            else if (entityDto is UsersRatings)
    //            {
    //                var userRating = entityDto as UsersRatings;
    //                userRating.Id = GenerateCompositeId(userRating.UsuarioId, userRating.RatingId);
    //            }

    //            await ValidateEntityIfNotExist(entityDto);
    //            var entity= _mapper.Map<T>(entityDto);
    //            var newEntity = await repository.AddAsync(entity);
    //        var newEntityDto = _mapper.Map<T>(newEntity);
    //        return newEntityDto;
    //    }
    //        public async Task <IEnumerable<T>> GetList()
    //        {
    //            return await repository.GetAllAsync();
    //        }
    //        public virtual async Task Delete(int id)
    //        {
    //            var entity = await repository.GetByIdAsync(id);
    //            if (entity == null)
    //                throw new ApplicationException($"Entity with id {id} not found.");

    //            await repository.DeleteAsync(entity);
    //        }

    //        protected virtual async Task ValidateEntityIfNotExist(T entity)
    //        {
    //            var existingEntity = await repository.GetByIdAsync(entity.Id);
    //            if (existingEntity != null)
    //                throw new ApplicationException($"Entity with id {entity.Id} already exists.");
    //        }

    //    protected int GenerateCompositeId(int firstId, int secondId)
    //    {
    //        // Convertir firstId y secondId a cadenas con ceros a la izquierda
    //        string formattedFirstId = firstId.ToString("D4"); // "D4" indica que queremos 4 dígitos, con ceros a la izquierda si es necesario
    //        string formattedSecondId = secondId.ToString("D4");

    //        // Concatenar los valores formateados de firstId y secondId
    //        string idString = $"1{formattedFirstId}{formattedSecondId}";

    //        // Intentar convertir la cadena en un entero
    //        if (int.TryParse(idString, out int id))
    //        {
    //            // La conversión fue exitosa, se devuelve el ID
    //            return id;
    //        }
    //        else
    //        {
    //            // La conversión falló, podría mostrar un mensaje de error o tomar alguna otra acción
    //            throw new ArgumentException("No se pudo generar el ID compuesto como un entero.");
    //        }
    //    }

    //}
}





