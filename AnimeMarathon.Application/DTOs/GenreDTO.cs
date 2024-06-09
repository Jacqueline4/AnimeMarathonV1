
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMarathon.Application.Services.DTOs
{
    public class GenreDTO : BaseDTO
    { 
         
        public string? Name { get; set; }

        public IEnumerable<AnimeMinDTO>? Animes{ get; set; }
    }
}
