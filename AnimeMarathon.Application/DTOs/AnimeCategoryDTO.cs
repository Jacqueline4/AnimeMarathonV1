
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMarathon.Application.Services.DTOs
{
    public class AnimeCategoryDTO : BaseDTO
    {
        [ForeignKey("CategoriaId")]
        public int CategoriaId { get; set; }
        [ForeignKey("AnimeId")]
        public int AnimeId { get; set; }

    }
}
