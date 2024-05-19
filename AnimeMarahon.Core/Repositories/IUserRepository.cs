using AnimeMarahon.Core.Entities;
using AnimeMarahon.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarahon.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserLoginAsync(string username, string password);

        Task<User> GetUserByNameAsync(string username);

    }
}
