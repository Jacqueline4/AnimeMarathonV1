using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.Interfaces;
using AnimeMarathon.Application.Services;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnimeMarathon.Application.Services.Base;
using AnimeMarathon.Application.Services.DTOs;

namespace AnimeMarathonV1.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            this.mapper = mapper;
            this.userService = userService;
        }
        /// <summary>
        /// Obtiene una lista de todos los usuarios.
        /// </summary>
        /// <remarks>
        /// Este método devuelve una lista de todos los usuarios registrados.
        /// </remarks>
        /// <returns>Una lista de objetos UserDTO.</returns>
        /// <response code="200">Devuelve la lista de usuarios.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<UserDTO>> Get()
        {
            var list = await userService.GetUserList(); 
            var mapped = mapper.Map<IEnumerable<UserDTO>>(list);
            return mapped;
        }

        /// <summary>
        /// Obtiene un usuario por su nombre.
        /// </summary>
        /// <remarks>
        /// Este método devuelve un usuario específico basado en su nombre.
        /// </remarks>
        /// <param name="userName">El nombre del usuario.</param>
        /// <returns>Un objeto UserDTO.</returns>
        /// <response code="200">Devuelve el usuario especificado.</response>
        /// <response code="404">Si no se encuentra un usuario con el nombre especificado.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpGet("GetUserByName/{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<UserDTO> GetUser(string userName) 
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                var user = await userService.GetUserByName(userName);
                var mapped = mapper.Map<UserDTO>(user);
                return mapped;
            }

            var userByName = await userService.GetUserByName(userName);
            var mappedByName = mapper.Map<UserDTO>(userByName);
            return mappedByName;
        }

        /// <summary>
        /// Autentica a un usuario.
        /// </summary>
        /// <remarks>
        /// Este método autentica a un usuario basado en su nombre de usuario y contraseña.
        /// </remarks>
        /// <param name="loginViewModel">El objeto UserDTO que contiene el nombre de usuario y la contraseña.</param>
        /// <returns>Una respuesta de acción.</returns>
        /// <response code="200">Si la autenticación es exitosa.</response>
        /// <response code="401">Si la autenticación falla.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthenticateUserAsync(UserDTO loginViewModel)
        {
            var user = await userService.AuthenticateUserAsync(loginViewModel.Name, loginViewModel.Password);

            if (user is not null)
            {
                return Ok(user);
            }
            else
            {
                return Unauthorized(new { error = "Nombre de usuario o contraseña incorrectos." });
            }
        }

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <remarks>
        /// Este método crea un nuevo usuario con la información proporcionada en el objeto UserDTO.
        /// </remarks>
        /// <param name="userViewModel">El objeto UserDTO que contiene la información del usuario a crear.</param>
        /// <returns>El objeto UserDTO creado.</returns>
        /// <response code="201">Devuelve el usuario recién creado.</response>
        /// <response code="400">Si el objeto enviado es nulo o inválido.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpPost("Registro")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<UserDTO> CreateUser(UserDTO userViewModel)
        {
            var mapped = mapper.Map<User>(userViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var entityDto = await userService.Create(userViewModel);

            var mappedViewModel = mapper.Map<UserDTO>(entityDto);
            return mappedViewModel;
        }

        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        /// <remarks>
        /// Este método actualiza un usuario existente con la información proporcionada en el objeto UserDTO.
        /// </remarks>
        /// <param name="userViewModel">El objeto UserDTO que contiene la información actualizada del usuario.</param>
        /// <response code="204">Indica que el usuario se ha actualizado correctamente.</response>
        /// <response code="400">Si el objeto enviado es nulo o inválido.</response>
        /// <response code="404">Si no se encuentra un usuario con el ID especificado.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task UpdateUser(UserDTO userViewModel)
        {
            var mapped = mapper.Map<User>(userViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            await userService.Update(userViewModel);
        }

        /// <summary>
        /// Elimina un usuario.
        /// </summary>
        /// <remarks>
        /// Este método elimina un usuario con el ID proporcionado.
        /// </remarks>
        /// <param name="userId">El ID del usuario a eliminar.</param>
        /// <response code="204">Indica que el usuario se ha eliminado correctamente.</response>
        /// <response code="400">Si el ID proporcionado es inválido.</response>
        /// <response code="404">Si no se encuentra un usuario con el ID proporcionado.</response>
        /// <response code="500">Si ocurre un error en el servidor.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task DeleteUser(int userId)
        {

            await userService.Delete(userId);
        }
    }
}
