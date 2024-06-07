using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.DTOs;
using AnimeMarathon.Application.Interfaces.Base;
using AnimeMarathon.Application.Services.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeMarathonV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBaseServices<CommentDTO, Comment> baseServices;
        public CommentsController(IMapper mapper, IBaseServices<CommentDTO, Comment> baseServices)
        {
            this.mapper = mapper;
            this.baseServices = baseServices;
        }
        /// <summary>
        /// Obtiene una lista de todos los comentarios.
        /// </summary>
        /// <remarks>
        /// Este método devuelve una lista de todos los comentarios disponibles.
        /// </remarks>
        /// <returns>Una lista de objetos CommentDTO.</returns>
        /// <response code="200">Devuelve la lista de comentarios.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CommentDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<CommentDTO>> Get()
        {
            var list = await baseServices.GetList();
            var mapped = mapper.Map<IEnumerable<CommentDTO>>(list);
            return mapped;
        }

        /// <summary>
        /// Crea un nuevo comentario.
        /// </summary>
        /// <remarks>
        /// Este método crea un nuevo comentario con la información proporcionada en el objeto CommentDTO.
        /// </remarks>
        /// <param name="commentViewModel">El objeto CommentDTO que contiene la información del comentario a crear.</param>
        /// <returns>El objeto CommentDTO creado.</returns>
        /// <response code="201">Devuelve el comentario recién creado.</response>
        /// <response code="400">Si el objeto enviado es nulo o inválido.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CommentDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CommentDTO> CreateComment(CommentDTO commentViewModel)
        {
            commentViewModel.DateTime = DateTime.Now;
            var mapped = mapper.Map<Comment>(commentViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var entityDto = await baseServices.Create(commentViewModel);

            var mappedViewModel = mapper.Map<CommentDTO>(entityDto);
            return mappedViewModel;
        }

        /// <summary>
        /// Elimina el comentario.
        /// </summary>
        /// <remarks>
        /// Este método elimina el comentario con el ID proporcionado.
        /// </remarks>
        /// <param name="animeCommentId">El ID del comentario a eliminar.</param>
        /// <response code="204">Indica que se ha eliminado correctamente.</response>
        /// <response code="400">Si el ID proporcionado es inválido.</response>
        /// <response code="404">Si no se encuentra el ID proporcionado.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task DeleteAnimeComment(int animeCommentId)
        {
            await baseServices.Delete(animeCommentId);
        }
    }
}
