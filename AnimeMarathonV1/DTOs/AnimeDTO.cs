using AnimeMarathonV1.DTOs.NewFolder;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMarathonV1.DTOs
{
    public class AnimeDTO : BaseDTO
    {
        [Column("Nombre")]
        public string Title { get; set; }
        [Column("Subtipo")]
        public string Suptype { get; set; }
        [Column("Valoracion_media")]
        public decimal? AverageRating { get; set; }
        [Column("Fecha_publicacion")]
        public DateTime StartDate { get; set; }
        [Column("Fecha_final")]
        public DateTime? EndDate { get; set; }
        [Column("Descripcion")]
        public string? Description { get; set; }
        [Column("Total_episodios")]
        public int TotalEpisodes { get; set; }
    }
}
