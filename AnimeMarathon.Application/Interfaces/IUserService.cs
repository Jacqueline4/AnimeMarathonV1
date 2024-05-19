using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUserById(int userId);
        Task<UserDTO> GetUserByName(string username);
        Task<IEnumerable<UserDTO>> GetUserList();
        Task<UserDTO> AuthenticateUserAsync(string username, string password);
        Task<UserDTO> Create(UserDTO user);
        Task Update(UserDTO user);
        Task Delete(int userId);
    }
}
