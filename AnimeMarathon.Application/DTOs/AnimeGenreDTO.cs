
using AnimeMarahon.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMarathon.Application.Services.DTOs
{
    public class AnimeGenreDTO : BaseDTO
    {


        [ForeignKey("GeneroId")]
        public int GeneroId { get; set; }

        public virtual GenreDTO Genero { get; set; }

        [ForeignKey("AnimeId")]
        public int AnimeId { get; set; }
        public virtual AnimeDTO Anime { get; set; }

    }
}
