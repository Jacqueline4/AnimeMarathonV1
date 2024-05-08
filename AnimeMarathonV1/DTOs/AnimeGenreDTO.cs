using AnimeMarahon.Core.Entities;
using AnimeMarathonV1.DTOs.NewFolder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMarathonV1.DTOs
{
    public class AnimeGenreDTO : BaseDTO
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("GeneroId")]
        public int GeneroId { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("AnimeId")]
        public int AnimeId { get; set; }

        // Agrega propiedades de navegación si es necesario
        //public virtual Anime Anime { get; set; }
        //public virtual Genre Genre { get; set; }
    }
}
