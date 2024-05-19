
using System.ComponentModel.DataAnnotations.Schema;


namespace AnimeMarathon.Application.Services.DTOs
{
    public class UsersRatingsDTO : BaseDTO
    {
        [ForeignKey("UsuarioId")]
        public int UsuarioId { get; set; }
        [ForeignKey("ValoracionId")]
        public int RatingId { get; set; }
    }
}
