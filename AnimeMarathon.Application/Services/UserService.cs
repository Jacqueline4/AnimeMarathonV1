using AnimeMarahon.Core.Entities;
using AnimeMarahon.Core.Repositories;
using AnimeMarathon.Application.Interfaces;
using AnimeMarathon.Application.Services.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AnimeMarathon.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> AuthenticateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetUserLoginAsync(username, password);
            //TODO throw Auth error message
            if(user is null)
            {
                throw new Exception("No existe usuario ");
            }
            return _mapper.Map<UserDTO>(user);
        }
        public async Task<IEnumerable<UserDTO>> GetUserList()
        {
            var userList = await _userRepository.GetAllAsync();
            var response = _mapper.Map<IEnumerable<UserDTO>>(userList);
            return response;
        }
        public async Task<UserDTO> Create(UserDTO userDto)
        {
            await ValidateUserIfExist(userDto);
            var user = _mapper.Map<User>(userDto);
            var newEntity = await _userRepository.AddAsync(user);

            var newEntityDto = _mapper.Map<UserDTO>(newEntity);
            return newEntityDto;
        }

        public async Task Delete(int userId)
        {
            var deletedUser = await _userRepository.GetByIdAsync(userId);
            if (deletedUser == null)
                throw new ApplicationException($"Entity could not be loaded.");

            await _userRepository.DeleteAsync(deletedUser);
        }

        public async Task<UserDTO> GetUserByName(string username)
        {
            var user = await _userRepository.GetUserByNameAsync(username);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task Update(UserDTO user)
        {
            //ValidateUserIfNotExist(user);

            var editUser = await _userRepository.GetByIdAsync(user.Id);
            if (editUser == null)
                throw new ApplicationException($"Entity could not be loaded.");

            editUser.Id = user.Id;
            editUser.Name = user.Name;
            editUser.LastName = user.LastName;
            editUser.Password = user.Password;
            editUser.Email = user.Email;

            await _userRepository.UpdateAsync(editUser);
        }
        private async Task ValidateUserIfExist(UserDTO user)
        {
            var existingEntity = await _userRepository.GetByIdAsync(user.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{user.ToString()} with this id already exists");
        }

        private void ValidateUserIfNotExist(UserDTO user)
        {
            var existingEntity = _userRepository.GetByIdAsync(user.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{user.ToString()} with this id is not exists");
        }
    }
}
