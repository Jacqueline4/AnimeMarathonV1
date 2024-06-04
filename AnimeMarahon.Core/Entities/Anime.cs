using AnimeMarahon.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarahon.Core.Entities
{
    [Table("Animes")]
    public class Anime :BaseEntity
    {
        public Anime() {

            UsersAnime = new HashSet<UsersAnimes>();
            AnimeGenres = new HashSet<AnimeGenre>();
            AnimeCategories = new HashSet<AnimeCategory>();
            AnimeRatings = new HashSet<Rating>();
        }  


        [Column("Nombre")]
        public string Title {  get; set; }
        [Column("Estado_emision")]
        public string Status { get; set; }
        [Column("Subtipo")]
        public string Subtype { get; set; } //ENUM_SUBTIPO
        [Column("AgeRating")]
        public string AgeRating { get; set; }
        [Column("Valoracion_media")]
        public decimal? AverageRating { get; set; }
        [Column("Fecha_publicacion")]
        public DateTime StartDate { get; set; }
        [Column("Fecha_final")]
        public DateTime? EndDate { get; set; }
        [Column("Descripcion")]
        public string? Description { get; set; }
        [Column("Total_episodios")]
        public int? TotalEpisodes { get; set; }
        [Column("PosterUrl")]
        public string posterImage { get; set; }

        

        public virtual IEnumerable<UsersAnimes> UsersAnime {  get; set; }

        public virtual IEnumerable<AnimeGenre> AnimeGenres {  get; set; }
        public virtual IEnumerable<AnimeCategory> AnimeCategories { get; set; }

        public virtual IEnumerable<Rating> AnimeRatings { get; set; }
        
    }
}
