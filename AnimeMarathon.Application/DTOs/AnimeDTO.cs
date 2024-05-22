using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMarathon.Application.Services.DTOs
{
    public class AnimeDTO : BaseDTO
    {
        [Column("Nombre")]
        public string Title { get; set; }
        [Column("Estado_emision")]
        public string Status { get; set; }
        [Column("Subtipo")]
        public string Subtype { get; set; }
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
        public int TotalEpisodes { get; set; }
        [Column("PosterUrl")]
        public string posterImage { get; set; }
    }
}
