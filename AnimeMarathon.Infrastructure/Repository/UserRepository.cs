using AnimeMarahon.Core.Entities;
using AnimeMarahon.Core.Repositories;
using AnimeMarathon.Data.Data;
using AnimeMarathon.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Data.Repository
{
    public class UserRepository : Repository<User>,IUserRepository
    {
        public UserRepository(AnimeMarathonContext dbContext) : base(dbContext)
        {
        }
        public async Task<User> GetUserLoginAsync(string username, string password)
        {
            var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Name == username);

            if (user != null && user.Password == password)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<User> GetUserByNameAsync(string username)
        {
            //Users.Include(x => x.UserAnimes).ThenInclude(x => x.Anime)
            return await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Name == username);
        }
    }
}
