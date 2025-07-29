using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.Interfaces;
using AnimeMarathon.Application.Interfaces.Base;
using AnimeMarathon.Application.Services;
using AnimeMarathon.Application.Services.Base;
using AnimeMarathon.Application.Services.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeMarathonV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimeCategoryController : ControllerBase
    {
        private readonly IBaseServices<AnimeCategoryDTO,AnimeCategory> baseServices ;
        public AnimeCategoryController(IBaseServices<AnimeCategoryDTO,AnimeCategory> baseServices)
        {
            this.baseServices= baseServices;
        }
        /// <summary>
        /// Obtiene una lista de las relaciones entre anime y categoría.
        /// </summary>
        /// <remarks>
        /// Este método devuelve una lista de todas las relaciones entre anime y categoría disponibles.
        /// </remarks>
        /// <returns>Una lista de objetos AnimeCategoryDTO.</returns>
        /// <response code="200">Devuelve la lista de relaciones entre anime y categoría.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AnimeCategoryDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<AnimeCategoryDTO>> Get()
        {
            return await baseServices.GetList();
        }
        /// <summary>
        /// Crea una nueva relación entre categoría y anime.
        /// </summary>
        /// <remarks>
        /// Este método crea una nueva relación de anime con categoría, con la información proporcionada en el objeto AnimeCategoryDTO.
        /// </remarks>
        /// <param name="ViewModel">El objeto AnimeCategoryDTO que contiene la información de la relación entre anime y categoría a crear.</param>
        /// <returns>El objeto AnimeCategoryDTO creado.</returns>
        /// <response code="201">Devuelve la relación entre anime y categoría recién creada.</response>
        /// <response code="400">Si el objeto enviado es nulo o inválido.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AnimeCategoryDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<AnimeCategoryDTO> Create(AnimeCategoryDTO ViewModel)
        {
            return await baseServices.Create(ViewModel);
        }
        /// <summary>
        /// Elimina una relación entre anime y categoría.
        /// </summary>
        /// <remarks>
        /// Este método elimina una relación entre anime y categoría con el ID proporcionado.
        /// </remarks>
        /// <param name="id">El ID de la relación entre anime y categoría a eliminar.</param>
        /// <response code="204">Indica que la relación entre anime y categoría se ha eliminado correctamente.</response>
        /// <response code="400">Si el ID proporcionado es inválido.</response>
        /// <response code="404">Si no se encuentra una relación entre anime y categoría con el ID proporcionado.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Delete(int id)
        {
            await baseServices.Delete(id);
        }
    }
}
