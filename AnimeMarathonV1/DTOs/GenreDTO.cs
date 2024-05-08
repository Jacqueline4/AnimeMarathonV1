using AnimeMarathonV1.DTOs.NewFolder;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMarathonV1.DTOs
{
    public class GenreDTO : BaseDTO
    {
        [Column("Nombre")]
        public string? Name { get; set; }

        [Column("Descripcion")]
        public string? Description { get; set; }
    }
}
