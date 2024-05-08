using AnimeMarahon.Core.Entities;
using AnimeMarathon.Data.Data;
using AnimeMarathon.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Data.Repository
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(AnimeMarathonContext dbContext) : base(dbContext)
        {
        }
    }
}
