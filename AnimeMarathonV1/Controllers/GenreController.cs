using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.Interfaces;
using AnimeMarathon.Application.Services;
using AnimeMarathon.Application.Services.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeMarathonV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService genreService;
        public GenreController( IGenreService genreService)
        {
            this.genreService= genreService;    
        }
        /// <summary>
        /// Obtiene una lista de todos los géneros.
        /// </summary>
        /// <remarks>
        /// Este método devuelve una lista de todos los géneros disponibles.
        /// </remarks>
        /// <returns>Una lista de objetos GenreDTO.</returns>
        /// <response code="200">Devuelve la lista de géneros.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GenreDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<GenreDTO>> Get()
        {
            return await genreService.GetGenreList();
        }

        /// <summary>
        /// Obtiene una lista de géneros por el ID del anime.
        /// </summary>
        /// <remarks>
        /// Este método devuelve una lista de géneros asociados a un anime específico.
        /// </remarks>
        /// <param name="animeId">El ID del anime.</param>
        /// <returns>Una lista de objetos GenreDTO.</returns>
        /// <response code="200">Devuelve la lista de géneros.</response>
        /// <response code="404">Si no se encuentran géneros para el anime especificado.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpGet("GetGenreByAnimeId/{animeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GenreDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<GenreDTO>> GetGenre(int animeId)
        {
            return await genreService.GetGenresByAnimeId(animeId);
        }

        /// <summary>
        /// Crea un nuevo género.
        /// </summary>
        /// <remarks>
        /// Este método crea un nuevo género con la información proporcionada en el objeto GenreDTO.
        /// </remarks>
        /// <param name="genreViewModel">El objeto GenreDTO que contiene la información del género a crear.</param>
        /// <returns>El objeto GenreDTO creado.</returns>
        /// <response code="201">Devuelve el género recién creado.</response>
        /// <response code="400">Si el objeto enviado es nulo o inválido.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenreDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GenreDTO> CreateGenre(GenreDTO genreViewModel)
        {
            return await genreService.Create(genreViewModel);
        }

        /// <summary>
        /// Actualiza un género existente.
        /// </summary>
        /// <remarks>
        /// Este método actualiza un género existente con la información proporcionada en el objeto GenreDTO.
        /// </remarks>
        /// <param name="genreViewModel">El objeto GenreDTO que contiene la información actualizada del género.</param>
        /// <response code="204">Indica que el género se ha actualizado correctamente.</response>
        /// <response code="400">Si el objeto enviado es nulo o inválido.</response>
        /// <response code="404">Si no se encuentra un género con el ID especificado.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task UpdateGenre(GenreDTO genreViewModel)
        {
            await genreService.Update(genreViewModel);
        }

        /// <summary>
        /// Elimina un género.
        /// </summary>
        /// <remarks>
        /// Este método elimina un género con el ID proporcionado.
        /// </remarks>
        /// <param name="genreId">El ID del género a eliminar.</param>
        /// <response code="204">Indica que el género se ha eliminado correctamente.</response>
        /// <response code="400">Si el ID proporcionado es inválido.</response>
        /// <response code="404">Si no se encuentra un género con el ID proporcionado.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task DeleteGenre(int genreId)
        {      
            await genreService.Delete(genreId);
        }
    }

}
