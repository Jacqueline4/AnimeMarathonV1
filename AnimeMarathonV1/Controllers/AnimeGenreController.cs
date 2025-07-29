using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.Interfaces;
using AnimeMarathon.Application.Interfaces.Base;
using AnimeMarathon.Application.Services.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeMarathonV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimeGenreController : ControllerBase
    {
        private readonly IBaseServices<AnimeGenreDTO,AnimeGenre> baseServices;
        public AnimeGenreController( IBaseServices<AnimeGenreDTO,AnimeGenre> baseServices) 
        {
            this.baseServices = baseServices;
        }
        /// <summary>
        /// Obtiene una lista de relaciones entre anime y género.
        /// </summary>
        /// <remarks>
        /// Este método devuelve una lista de todas las relaciones entre anime y género disponibles.
        /// </remarks>
        /// <returns>Una lista de objetos AnimeGenreDTO.</returns>
        /// <response code="200">Devuelve la lista relaciones entre anime y género.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AnimeGenreDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<AnimeGenreDTO>> Get()
        {
            return await baseServices.GetList();
        }
        /// <summary>
        /// Crea una relación entre anime y género.
        /// </summary>
        /// <remarks>
        /// Este método crea una relación entre anime y género con la información proporcionada en el objeto AnimeGenreDTO.
        /// </remarks>
        /// <param name="animeGenreViewModel">El objeto AnimeGenreDTO que contiene la información relación entre anime y género a crear.</param>
        /// <returns>El objeto AnimeGenreDTO creado.</returns>
        /// <response code="201">Devuelve la relación entre anime y género recién creado.</response>
        /// <response code="400">Si el objeto enviado es nulo o inválido.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AnimeGenreDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<AnimeGenreDTO> CreateAG(AnimeGenreDTO animeGenreViewModel)
        {
            return await baseServices.Create(animeGenreViewModel);
        }

        /// <summary>
        /// Elimina la relación entre anime y género.
        /// </summary>
        /// <remarks>
        /// Este método elimina la relación de género con anime con el ID proporcionado.
        /// </remarks>
        /// <param name="animeGenreId">El ID la relación entre anime y género a eliminar.</param>
        /// <response code="204">Indica quela relación entre anime y género se ha eliminado correctamente.</response>
        /// <response code="400">Si el ID proporcionado es inválido.</response>
        /// <response code="404">Si no se encuentra una la relación entre anime y género con el ID proporcionado.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task DeleteAnimeGenre(int animeGenreId)
        {
            await baseServices.Delete(animeGenreId);
        }
    }
}
