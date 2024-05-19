using AnimeMarathon.Application.Services.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Application.DTOs
{
    public class RatingDTO : BaseDTO
    {
        [Column("Valoracion_media_propia")]
        public decimal? AverageRatingSelf { get; set; }
        [ForeignKey("AnimeId")]
        public int AnimeId { get; set; }
        [Column("UsuarioId")]
        public int UserId { get; set; }
    }
}
