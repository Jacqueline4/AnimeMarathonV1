using AnimeMarahon.Core.Entities;
using AnimeMarahon.Core.Repositories;
using AnimeMarathon.Data.Data;
using AnimeMarathon.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Data.Repository
{
    public class AnimeGenreRepository : Repository<AnimeGenre>, IAnimeGenreRepository
    {
        public AnimeGenreRepository (AnimeMarathonContext dbContext) : base(dbContext)
        {
        }
    }
}
