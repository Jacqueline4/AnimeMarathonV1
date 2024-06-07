using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.Interfaces;
using AnimeMarathon.Application.Services.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeMarathonV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;
        public CategoryController(IMapper mapper, ICategoryService categoryService)
        {
            this.mapper = mapper;
            this.categoryService = categoryService;
        }

        /// <summary>
        /// Obtiene las categorías asociadas a un anime por su ID.
        /// </summary>
        /// <remarks>
        /// Este método devuelve una lista de categorías asociadas al anime con el ID proporcionado.
        /// </remarks>
        /// <param name="animeId">El ID del anime.</param>
        /// <returns>Una lista de objetos CategoryDTO.</returns>
        /// <response code="200">Devuelve la lista de categorías asociadas al anime.</response>
        /// <response code="400">Si el ID proporcionado es inválido.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpGet("GetCategoryByAnimeId/{animeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<CategoryDTO>> GetCategoryByAnimeId(int animeId)
        {
            var listByName = await categoryService.GetCategoryByAnimeId(animeId); 
            var mappedByName = mapper.Map<IEnumerable<CategoryDTO>>(listByName);
            return mappedByName;
        }
        /// <summary>
        /// Obtiene una lista de todas las categorías.
        /// </summary>
        /// <remarks>
        /// Este método devuelve una lista de todas las categorías disponibles.
        /// </remarks>
        /// <returns>Una lista de objetos CategoryDTO.</returns>
        /// <response code="200">Devuelve la lista de categorías.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {      
                var list = await categoryService.GetCategoryList();
                var mapped = mapper.Map<IEnumerable<CategoryDTO>>(list);
                return mapped;
        }

        /// <summary>
        /// Crea una nueva categoría.
        /// </summary>
        /// <remarks>
        /// Este método crea una nueva categoría con la información proporcionada en el objeto CategoryDTO.
        /// </remarks>
        /// <param name="categoryViewModel">El objeto CategoryDTO que contiene la información de la categoría a crear.</param>
        /// <returns>El objeto CategoryDTO creado.</returns>
        /// <response code="201">Devuelve la categoría recién creada.</response>
        /// <response code="400">Si el objeto enviado es nulo o inválido.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoryDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CategoryDTO> CreateCategory(CategoryDTO categoryViewModel)
        {
            var mapped = mapper.Map<CategoryDTO>(categoryViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var entityDto = await categoryService.Create(mapped);

            var mappedViewModel = mapper.Map<CategoryDTO>(entityDto);
            return mappedViewModel;
        }

        /// <summary>
        /// Actualiza una categoría existente.
        /// </summary>
        /// <remarks>
        /// Este método actualiza una categoría con la información proporcionada en el objeto CategoryDTO.
        /// </remarks>
        /// <param name="categoryViewModel">El objeto CategoryDTO que contiene la información de la categoría a actualizar.</param>
        /// <response code="204">Indica que la categoría se ha actualizado correctamente.</response>
        /// <response code="400">Si el objeto enviado es nulo o inválido.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task UpdateCategory(CategoryDTO categoryViewModel)
        {
            var mapped = mapper.Map<CategoryDTO>(categoryViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            await categoryService.Update(mapped);
        }

        /// <summary>
        /// Elimina una categoría.
        /// </summary>
        /// <remarks>
        /// Este método elimina una categoría con el ID proporcionado.
        /// </remarks>
        /// <param name="categoryId">El ID de la categoría a eliminar.</param>
        /// <response code="204">Indica que la categoría se ha eliminado correctamente.</response>
        /// <response code="400">Si el ID proporcionado es inválido.</response>
        /// <response code="404">Si no se encuentra una categoría con el ID proporcionado.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task DeleteCategory(int categoryId)
        {
            await categoryService.Delete(categoryId);
        }
    }
}
