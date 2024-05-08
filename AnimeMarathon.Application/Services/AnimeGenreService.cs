
using AnimeMarahon.Core.Entities;
using AnimeMarahon.Core.Repositories;
using AnimeMarathon.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Application.Services
{
    public class AnimeGenreService : IAnimeGenreService
    {
        private readonly IAnimeGenreRepository animeGenrerepository;

        public AnimeGenreService(IAnimeGenreRepository animeGenrerepository)
        {
            this.animeGenrerepository = animeGenrerepository;
        }
        public async Task<AnimeGenre> Create(AnimeGenre ag)
        {
            await ValidateAnimeGenreIfExist(ag);

            var newEntity = await animeGenrerepository.AddAsync(ag);
            return newEntity;
        }

        public async Task Delete(int agId)
        {
            var deletedAnimeGenre = await animeGenrerepository.GetByIdAsync(agId);
            if (deletedAnimeGenre == null)
                throw new ApplicationException($"Entity could not be loaded.");

            await animeGenrerepository.DeleteAsync(deletedAnimeGenre);
        }

        public async Task<IEnumerable<AnimeGenre>> GetAnimeGenreList()
        {
            var animeGenreList = await animeGenrerepository.GetAllAsync();
            return animeGenreList;
        }

        private async Task ValidateAnimeGenreIfExist(AnimeGenre ag)
        {
            var existingEntity = await animeGenrerepository.GetByIdAsync(ag.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{ag.ToString()} with this id already exists");
        }
    }
}
