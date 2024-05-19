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
        [HttpGet]
        public async Task<IEnumerable<UserDTO>> Get()
        {
            var list = await userService.GetUserList(); 
            var mapped = mapper.Map<IEnumerable<UserDTO>>(list);
            return mapped;
        }

        [HttpGet("GetUserByName/{userName}")]
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

        [HttpPost("Login")]
        public async Task<IActionResult> AuthenticateUserAsync(UserDTO loginViewModel)
        {
            var user = await userService.AuthenticateUserAsync(loginViewModel.Name, loginViewModel.Password);

            if (user is not null)
            {
                return Ok(user); // Autenticación exitosa, devuelve el usuario
            }
            else
            {
                return Unauthorized(new { error = "Nombre de usuario o contraseña incorrectos." }); // Autenticación fallida, devuelve un mensaje de error
            }
        }

        [HttpPost("Registro")]
        public async Task<UserDTO> CreateUser(UserDTO userViewModel)
        {
            var mapped = mapper.Map<User>(userViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var entityDto = await userService.Create(userViewModel);

            var mappedViewModel = mapper.Map<UserDTO>(entityDto);
            return mappedViewModel;
        }

        [HttpPut]
        public async Task UpdateUser(UserDTO userViewModel)
        {
            var mapped = mapper.Map<User>(userViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            await userService.Update(userViewModel);
        }

        [HttpDelete]
        public async Task DeleteUser(int userId)
        {

            await userService.Delete(userId);
        }
    }
}
