
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMarathon.Application.Services.DTOs
{
    public class UsersAnimeDTO : BaseDTO
    {
        [ForeignKey("AnimeId")]
        public int AnimeId { get; set; }
        [ForeignKey("UsuarioId")]
        public int UsuarioId { get; set; }
        [Column("Estado")]
        public string? Status { get; set; }
    }
}
