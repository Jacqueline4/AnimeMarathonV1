using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMarathon.Application.Services.DTOs
{
    public class CategoryDTO : BaseDTO
    {
        [Column("Nombre")]
        public string? Name { get; set; }

        public IEnumerable<AnimeMinDTO>? Animes { get; set; }

    }
}
