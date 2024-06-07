using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.DTOs;
using AnimeMarathon.Application.Interfaces;
using AnimeMarathon.Application.Interfaces.Base;
using AnimeMarathon.Application.Services;
using AnimeMarathon.Application.Services.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace AnimeMarathonV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAnimeService animeService;
        private readonly IBaseServices<UsersAnimeDTO,UsersAnimes> baseServices;
        public AnimeController(IMapper mapper, IAnimeService animeService, IBaseServices<UsersAnimeDTO,UsersAnimes> baseServices)
        {
            this.mapper= mapper;
            this.animeService = animeService;
            this.baseServices = baseServices;
        }
        /// <summary>
        /// Obtiene una lista de todos los animes disponibles en la base de datos.
        /// </summary>
        /// <returns>Una colección de objetos AnimeDTO que representan todos los animes disponibles.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AnimeDTO>))]
        [Produces("application/json")]
        public async Task<IEnumerable<AnimeDTO>> Get()
        {
            var list = await animeService.GetAnimeList();
            var mapped = mapper.Map<IEnumerable<AnimeDTO>>(list);
            return mapped;

        }

        //[HttpGet ("Paginate")]
        //public async Task<ActionResult<IEnumerable<AnimeDTO>>> GetAnimes(int pageNumber, int pageSize)
        //{
        //    //pageSize += pageSize;
        //    var animes = await animeService.GetAnimeListPag(pageNumber, pageSize);
        //    var mapped = mapper.Map<IEnumerable<AnimeDTO>>(animes);
        //    return Ok(mapped);
        //}

        /// <summary>
        /// Obtiene una lista de animes filtrada según los criterios proporcionados.
        /// </summary>
        /// <remarks>
        /// Permite realizar una búsqueda avanzada de animes utilizando diferentes criterios de filtrado.
        /// </remarks>
        /// <param name="data">Los criterios de filtrado para la búsqueda de animes.</param>
        /// <returns>Una colección de objetos <see cref="AnimeDTO"/> que cumplen con los criterios de búsqueda especificados.</returns>
        [HttpPost("GetAnimes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AnimeDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IEnumerable<AnimeDTO>> GetAnimesPost([FromBody] AnimeFilterDTO data)
        {  
             
            var listByName = await animeService.GetAnimeFilteredGeneric(data);
            var mappedByName = mapper.Map<IEnumerable<AnimeDTO>>(listByName);
            return mappedByName;
        }

        /// <summary>
        /// Obtiene una lista de animes cuyos nombres contienen el texto proporcionado.
        /// </summary>
        /// <remarks>
        /// Permite buscar animes cuyos nombres contienen una cadena específica.
        /// Si no se proporciona ningún nombre de anime, devuelve una lista de todos los animes disponibles.
        /// </remarks>
        /// <param name="animeName">El texto que debe contener el nombre de los animes a buscar.</param>
        /// <returns>Una colección de objetos <see cref="AnimeDTO"/> cuyos nombres coinciden con el texto proporcionado.</returns>
        [HttpGet("GetAnimeByName/{animeName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AnimeDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IEnumerable<AnimeDTO>> GetAnimes(string animeName)
        {
            if (string.IsNullOrWhiteSpace(animeName))
            {
                var list = await animeService.GetAnimeList();
                var mapped = mapper.Map<IEnumerable<AnimeDTO>>(list);
                return mapped;
            }

            var listByName = await animeService.GetAnimeByName(animeName);
            var mappedByName = mapper.Map<IEnumerable<AnimeDTO>>(listByName);
            return mappedByName;
        }
        /// <summary>
        /// Obtiene un anime por su identificador único.
        /// </summary>
        /// <param name="animeId">El identificador único del anime a buscar.</param>
        /// <returns>El objeto <see cref="AnimeDTO"/> correspondiente al anime con el ID especificado.</returns>
        /// <response code="200">Se devuelve el anime correspondiente al ID proporcionado.</response>
        /// <response code="404">No se encontró ningún anime con el ID especificado.</response>
        [HttpGet("GetAnimeById/{animeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AnimeDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<AnimeDTO> GetAnimeById(int animeId)
        {
            var anime = await animeService.GetAnimeById(animeId);
            var mapped = mapper.Map<AnimeDTO>(anime);
            return mapped;
        }
        /// <summary>
        /// Recupera una lista de animes para un usuario dado.
        /// </summary>
        /// <param name="userId">El ID del usuario.</param>
        /// <returns>Una lista de objetos AnimeDTO.</returns>
        /// <response code="200">Devuelve la lista de animes</response>
        /// <response code="400">Si el userId no es válido</response>
        /// <response code="500">Si hubo un error interno en el servidor</response>
        [HttpGet("GetAnimeByUserId/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<AnimeDTO>> GetAnimeByUserId(int userId)
        {
            try
            {
                var animes = await animeService.GetAnimeByUser(userId);
                return animes;
            }
            catch
            {
                return Enumerable.Empty<AnimeDTO>();
            }
        }

        /// <summary>
        /// Recupera una lista de animes según el nombre del género.
        /// </summary>
        /// <param name="genre">El nombre del género.</param>
        /// <returns>Una lista de objetos AnimeDTO.</returns>
        /// <response code="200">Devuelve la lista de animes</response>
        /// <response code="400">Si el género no es válido</response>
        /// <response code="500">Si hubo un error interno en el servidor</response>
        [HttpGet("GetAnimesByGenre/{genre}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<AnimeDTO>> GetAnimesByGenre(string genre)
        {
            var anime = await animeService.GetAnimeByGenre(genre);
            //var mapped = mapper.Map<AnimeDTO>(anime);
            return anime;
        }
        /// <summary>
        /// Recupera una lista de animes según el nombre de la categoría.
        /// </summary>
        /// <param name="categoryName">El nombre de la categoría.</param>
        /// <returns>Una lista de objetos AnimeDTO.</returns>
        /// <response code="200">Devuelve la lista de animes</response>
        /// <response code="400">Si el nombre de la categoría no es válido</response>
        /// <response code="500">Si hubo un error interno en el servidor</response>
        [HttpGet("GetAnimesByCategory/{categoryName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<AnimeDTO>> GetAnimeByCategory(string categoryName)
        {
            var anime = await animeService.GetAnimeByGenre(categoryName);
            return anime;
        }
        /// <summary>
        /// Recupera todos los comentarios según el ID del anime.
        /// </summary>
        /// <param name="animeId">El ID del anime.</param>
        /// <returns>Una lista de objetos CommentDTO.</returns>
        /// <response code="200">Devuelve la lista de comentarios</response>
        /// <response code="400">Si el ID del anime no es válido</response>
        /// <response code="500">Si hubo un error interno en el servidor</response>
        [HttpGet("GetCommentByAnime/{animeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<CommentDTO>> GetCommentsByAnimeId(int animeId)
        {
            var comments = await animeService.GetCommentsByAnimeId(animeId);
            var mapped = mapper.Map<IEnumerable<CommentDTO>>(comments);
            return mapped;
        }
        /// <summary>
        /// Recupera un anime de un usuario específico según el ID del usuario y el ID del anime.
        /// </summary>
        /// <param name="userId">El ID del usuario.</param>
        /// <param name="animeId">El ID del anime.</param>
        /// <returns>Un objeto UsersAnimeDTO.</returns>
        /// <response code="200">Devuelve el anime del usuario</response>
        /// <response code="404">Si el anime no se encuentra para el usuario dado</response>
        /// <response code="500">Si hubo un error interno en el servidor</response>
        [HttpGet("{userId}/{animeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsersAnimeDTO>> GetUsersAnimes(int userId, int animeId)
        {
            var userAnimes = await baseServices.GetList();

            var existingUserAnime = userAnimes.FirstOrDefault(ua => ua.UsuarioId == userId && ua.AnimeId == animeId);

            if (existingUserAnime != null)
            {
                return existingUserAnime;
            }
            
            return NotFound();
        }


        /// <summary>
        /// Crea en la base de datos la relacion entre anime y usuario.
        /// </summary>
        /// <param name="ViewModel">Los datos de la relación entre anime y usuario que se van a crear.</param>
        /// <returns>Los datos de la relación entre anime y usuario creada en la base de datos.</returns>
        [HttpPost ("UserAnime")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersAnimeDTO))]
        public async Task<UsersAnimeDTO> CreateUA(UsersAnimeDTO ViewModel)
        {
            var mapped = mapper.Map<UsersAnimes>(ViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var entityDto = await baseServices.Create(ViewModel);

            var mappedViewModel = mapper.Map<UsersAnimeDTO>(entityDto);
            return mappedViewModel;
        }

        /// <summary>
        /// Crea un nuevo anime.
        /// </summary>
        /// <param name="animeViewModel">El objeto AnimeDTO que representa el anime a crear.</param>
        /// <returns>El objeto AnimeDTO creado.</returns>
        /// <response code="201">Devuelve el anime creado</response>
        /// <response code="400">Si el objeto AnimeDTO es nulo o no válido</response>
        /// <response code="500">Si hubo un error interno en el servidor</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<AnimeDTO> CreateAnime(AnimeDTO animeViewModel)
        {
            var mapped = mapper.Map<Anime>(animeViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var entityDto = await animeService.Create(animeViewModel);

            var mappedViewModel = mapper.Map<AnimeDTO>(entityDto);
            return mappedViewModel;
        }
        /// <summary>
        /// Actualiza el estado de un anime para un usuario.
        /// </summary>
        /// <param name="animeViewModel">El objeto UsersAnimeDTO que contiene la información actualizada del anime del usuario.</param>
        /// <returns>El objeto UsersAnimeDTO actualizado.</returns>
        /// <response code="200">Devuelve el anime del usuario actualizado</response>
        /// <response code="400">Si el objeto UsersAnimeDTO es nulo o no válido</response>
        /// <response code="500">Si hubo un error interno en el servidor</response>
        [HttpPut("UserAnime")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<UsersAnimeDTO> UpdateStatusUserAnime(UsersAnimeDTO animeViewModel)
        {
           
            var mapped = mapper.Map<UsersAnimes>(animeViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var entityDto = await baseServices.Update(animeViewModel);

            var mappedViewModel = mapper.Map<UsersAnimeDTO>(entityDto);
            return mappedViewModel;
        }
        /// <summary>
        /// Actualiza un anime existente.
        /// </summary>
        /// <param name="animeViewModel">El objeto AnimeDTO que contiene la información actualizada del anime.</param>
        /// <response code="204">La actualización se realizó correctamente</response>
        /// <response code="400">Si el objeto AnimeDTO es nulo o no válido</response>
        /// <response code="500">Si hubo un error interno en el servidor</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task UpdateAnime(AnimeDTO animeViewModel)
        {
            var mapped = mapper.Map<Anime>(animeViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            await animeService.Update(animeViewModel);
        }
        /// <summary>
        /// Elimina un anime existente.
        /// </summary>
        /// <param name="animeId">El ID del anime que se va a eliminar.</param>
        /// <response code="204">La eliminación se realizó correctamente</response>
        /// <response code="404">Si el anime no se encuentra</response>
        /// <response code="500">Si hubo un error interno en el servidor</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task DeleteAnime(int animeId)
        {
            await animeService.Delete(animeId);
        }
    }
}
