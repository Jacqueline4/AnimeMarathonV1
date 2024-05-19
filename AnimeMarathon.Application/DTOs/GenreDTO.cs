using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMarathon.Application.Services.DTOs
{
    public class GenreDTO : BaseDTO
    {
        [Column("Nombre")]
        public string? Name { get; set; }

       
    }
}
